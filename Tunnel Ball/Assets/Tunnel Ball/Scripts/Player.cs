﻿using System.Collections;
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

        //Raycast Death
        rayObj = GameObject.Find("PlayerRayCast");
        rayObj.transform.position = transform.position;
    }

    void Update()
    {
        //Marble Rotation
        RotatingMarble();

        //Mobile collision bug
        CollisionChecker(rayObj);
    }

    void OnTriggerEnter(Collider other)
    {
        DeathTrigger(other);
        DifferentTunnel(other);
    }

    #region Player Rotation

    [Header("Player Rotation")]
    public float rotateSpeed = 40f;
    public float startSpeed;
    float tractionSpeed;
	public float rotationCap = 40;
	//private Quaternion targetRotation;

    //Circle Rotation Variables
    private float maxVelocity;
    private float speedMultiplier;

    private void RotatingMarble()
    {

        //Caps the max rotationSpeed
        if (tractionSpeed > rotationCap)
        {
            tractionSpeed = rotationCap;
        }
        else
        {
            //Radius of the circle
            float radiusSquared = Mathf.Pow(maxVelocity, 2f);
            //Calculates y^2
            float ySquared = radiusSquared - Mathf.Pow(Time.timeSinceLevelLoad - maxVelocity, 2f);
            //Current Velocity with multiplier (increasing multiplier increases rate of speed);
            tractionSpeed = (speedMultiplier * 0.1f * ySquared) / (1/rotateSpeed) + startSpeed;
        }

        //Freezes rotation when game is paused
        if (Time.timeScale == 1)
        {
            //Applies rotation to Marble
            transform.Rotate(new Vector3(tractionSpeed * 10f * Time.deltaTime, 0, 0));
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
        if (other.tag == "Obstacle")
        {
            Die();
        }
    }

    void Die()
    {
        GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnels");

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
        for (int i = 0; i <= marbleShards.transform.childCount; i++)
        {
            //Applies force to each child of Marble Shard Parent
            GameObject shard = marbleShards.transform.GetChild(i).gameObject;
            PlayerCorpseImpulse(shard);
            i++;
        }
    }

    private void PlayerCorpseImpulse(GameObject shard)
    {
        thrust = forceMultiplier * Time.timeSinceLevelLoad * Time.deltaTime;
        rb = shard.transform.GetComponent<Rigidbody>();
        rb.AddForce(heightThrust/2, heightThrust, 0, ForceMode.Impulse);
        rb.AddForce(0, 0, thrust + Mathf.Pow(heightThrust, 1.5f), ForceMode.VelocityChange);
    }

    #region CollisionCheck

    [HideInInspector]
    GameObject rayObj;

    public void CollisionChecker(GameObject marble)
    {
        //Casts Ray from Marble's position
        float velocity = GameObject.FindGameObjectWithTag("Tunnels").GetComponent<Tunnel>().currentVelocity;
        float distance = velocity * Time.deltaTime;
        Vector3 bk = marble.transform.TransformDirection(Vector3.back * distance);

        Debug.DrawRay(marble.transform.position, bk, Color.red);

        //Collision checker
        RaycastHit objectHit;
        if (Physics.Raycast(marble.transform.position, bk, out objectHit, distance))
        {
            if (objectHit.transform.tag == "Obstacle")
            {
                ////Makes sure player doesnt pass through obstacle
                //GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnels");
                //foreach (GameObject tunnel in tunnels)
                //{
                //    tunnel.transform.position = new Vector3(tunnel.transform.position.x, tunnel.transform.position.y, tunnel.transform.position.z + distance);
                //}

                //Kills player
                Die();
            }
        }       
    }

    #endregion

    #endregion

}
