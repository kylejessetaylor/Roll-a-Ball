using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    void Start()
    {
        //Marble Rotation
        targetRotation = transform.rotation;
    }

    void Update()
    {
        //Marble Rotation
        RotatingMarble();
    }

    void OnTriggerEnter(Collider other)
    {
        DeathTrigger(other);
        DifferentTunnel(other);
    }

    /// <summary>
    /// Functions
    /// </summary>

    #region Player Rotation
    [Header("Player Rotation")]
    public float rotateSpeed = 40f;
	float tractionSpeed;
	float timer;
	public float rotationCap = 40;
	private Quaternion targetRotation;

    private void RotatingMarble()
    {
        timer = Time.timeSinceLevelLoad;

        //Caps the max rotationSpeed
        if (timer >= rotationCap)
        {
            timer = rotationCap;
        }

        //Marble rotation that increases on change in time.
        tractionSpeed = rotateSpeed * timer * Time.deltaTime;
        transform.Rotate(new Vector3(tractionSpeed, 0, 0));
    }
    #endregion

    #region InstantiateTrigger
    [Header("Tunnel Instantiation")]
    public List<GameObject> tunnelPieceList = new List<GameObject>();

    [HideInInspector]
    public GameObject tunnelPiece;
    //A counter for all the pieces spawned
    public int tunnelPieceCounter = 0;
    //Depth of each tunnel piece
    public int depthOfTunnelPiece = 200;
    //How many pieces we want to pull
    public int numberOfTunnelPieces = 2;

    public float playerPositionCounter = 0;

    //Records last tunnel spawned
    private GameObject lastTunnel;

    private void DifferentTunnel(Collider other)
    {
        if (other.tag == "Trigger") {
            GameObject newTunnel = tunnelPieceList[Random.Range(0, tunnelPieceList.Count)];

            //Allows Spawn
            //Debug for first tunnel spawn
            if (lastTunnel == null)
            {
                lastTunnel = newTunnel;

                //Builds Tunnel
                BuildLevel(newTunnel);

                return;
            }
            //Pick another Tunnel
            else if (lastTunnel.name == newTunnel.name)
            {
                DifferentTunnel(other);
            }
            else
            {
                lastTunnel = newTunnel;

                //Builds Tunnel
                BuildLevel(newTunnel);

                return;
            }
        }
    }


    public void BuildLevel(GameObject tunnelPieceToPlace)
    {
        GameObject newTunnel = TrashMan.spawn(tunnelPieceToPlace, (Vector3.forward * depthOfTunnelPiece), Quaternion.identity);
    }
    #endregion

    #region DeathTrigger

    [Header("Death Trigger")]
    //Marble Shards
    public GameObject deadPlayer;
    private Rigidbody rb;
    public float forceMultiplier = 20f;
    private float thrust;
    private bool force = true;

    //On & Off UI
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject deathUI;

    //Object that Tunnel's read rotation off & tunnel movement.
    private GameObject controller;
    private GameObject[] tunnels;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Controls");
    }

    //Turns off Ball & Tunnel Movement
    void DeathTrigger(Collider other)
    {
        tunnels = GameObject.FindGameObjectsWithTag("Tunnels");
        if (other.tag == "Obstacle")
        {
            //Tells how long play session was
            Debug.Log(Time.timeSinceLevelLoad);

            //Turns restart UI on
            inGameUI.gameObject.SetActive(false);
            deathUI.SetActive(true);

            //Deactivates Tunnel movement
            Cursor.visible = true;
            controller.GetComponent<Controls>().enabled = false;
            foreach (GameObject tunnel in tunnels)
            {
                tunnel.GetComponent<Tunnel>().enabled = false;
            }

            //Turns off Player
            gameObject.SetActive(false);
            GameObject marbleShards = Instantiate(deadPlayer);
            for(int i = 0; i <= marbleShards.transform.childCount; i++)
            {
                //Applies force to each child of Marble Shard Parent
                GameObject shard = marbleShards.transform.GetChild(i).gameObject;
                PlayerCorpseImpulse(shard);
                i++;
                Debug.Log(marbleShards.transform.childCount);
            }
            
        }
    }

    private void PlayerCorpseImpulse(GameObject shard)
    {
        thrust = forceMultiplier * Time.timeSinceLevelLoad * Time.deltaTime;
        rb = shard.transform.GetComponent<Rigidbody>();
        rb.AddForce(0f, 0f, thrust, ForceMode.Impulse);
        force = false;
    }

    #endregion

}
