using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    void Start()
    {
        //Marble Rotation
        maxVelocity = 100;
        speedMultiplier = 0.15f;

        //Tunnel Spawns
        lastTunnel = "Tunnel_001";
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
    public float startSpeed;
    float tractionSpeed;
	float timer;
	public float rotationCap = 40;
	//private Quaternion targetRotation;

    //Circle Rotation Variables
    private float maxVelocity;
    private float speedMultiplier;

    private void RotatingMarble()
    {
        timer = Time.timeSinceLevelLoad;

        //Caps the max rotationSpeed
        if (timer >= rotationCap)
        {
            timer = rotationCap;
        }


        //tractionSpeed = rotateSpeed * timer * Time.deltaTime;

        ///Marble rotation that increases on change in time.
        //Radius of the circle
        float radiusSquared = Mathf.Pow(maxVelocity, 2f);
        //Calculates y^2
        float ySquared = radiusSquared - Mathf.Pow(Time.timeSinceLevelLoad - maxVelocity, 2f);
        //Current Velocity with multiplier (increasing multiplier increases rate of speed);
        tractionSpeed = (speedMultiplier * 0.1f * ySquared)/rotateSpeed + startSpeed;

        //Freezes rotation when game is paused
        if (Time.timeScale == 1)
        {
            //Applies rotation to Marble
            transform.Rotate(new Vector3(tractionSpeed, 0, 0));
        }
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
    [HideInInspector]
    public int numberOfTunnelPieces = 2;

    public float playerPositionCounter = 0;

    //Records last tunnel spawned
    //[HideInInspector]
    private string lastTunnel;

    void DifferentTunnel(Collider other)
    {
        if (other.tag == "Trigger") {
            GameObject newTunnel = tunnelPieceList[Random.Range(0, tunnelPieceList.Count)];

            //Allows Spawn
            //Pick another Tunnel
            if (lastTunnel == newTunnel.name)
            {
                DifferentTunnel(other);
            }
            else
            {
                lastTunnel = newTunnel.name;

                //Builds Tunnel
                BuildLevel(newTunnel);

                return;
            }
        }
    }


    public void BuildLevel(GameObject tunnelPieceToPlace)
    {
        TrashMan.spawn(tunnelPieceToPlace, (Vector3.forward * depthOfTunnelPiece), Quaternion.identity);
    }

    #endregion

    #region DeathTrigger

    [Header("Death Trigger")]
    //Marble Shards
    public GameObject deadPlayer;
    private Rigidbody rb;
    public float forceMultiplier = 20f;
    public float heightThrust;
    private float thrust;
    private bool force = true;

    ////On & Off UI
    //[SerializeField] private GameObject inGameUI;
    //[SerializeField] private GameObject deathUI;

    //Object that Tunnel's read rotation off & tunnel movement.
    private GameObject controller;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Controls");
    }

    //Turns off Ball & Tunnel Movement
    void DeathTrigger(Collider other)
    {
        GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnels");
        if (other.tag == "Obstacle")
        {
            //Tells how long play session was
            Debug.Log("Player lasted " + Time.timeSinceLevelLoad + " seconds.");

            ////Turns restart UI on
            //inGameUI.SetActive(false);
            //deathUI.SetActive(true);

            //Deactivates Tunnel movement
            Cursor.visible = true;
            controller.GetComponent<Controls>().enabled = false;
            foreach (GameObject tunnel in tunnels)
            {
                tunnel.GetComponent<Tunnel>().enabled = false;
            }

            //Turns off Player
            gameObject.SetActive(false);
            //Spawns Shards
            GameObject marbleShards = Instantiate(deadPlayer);
            marbleShards.transform.position = transform.position;
            //Single force to all shards
            force = false;
            for (int i = 0; i <= marbleShards.transform.childCount; i++)
            {
                //Applies force to each child of Marble Shard Parent
                GameObject shard = marbleShards.transform.GetChild(i).gameObject;
                PlayerCorpseImpulse(shard);
                i++;
            }
        }
    }

    private void PlayerCorpseImpulse(GameObject shard)
    {
        thrust = forceMultiplier * Time.timeSinceLevelLoad * Time.deltaTime;
        rb = shard.transform.GetComponent<Rigidbody>();
        rb.AddForce(0f, heightThrust, 0, ForceMode.Impulse);
        rb.AddForce(0, 0, thrust + heightThrust, ForceMode.VelocityChange);
    }

    #endregion

}
