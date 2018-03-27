using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpiral : MonoBehaviour {

    public GameObject buttonManager;

    [Header("Position")]
    public float circleWidth = 9.25f;
    public float lerpTime = 0.2f;
    //Height
    public float minHeight = -1.47f;
    private float centeredPrct;
    private float timer;

    [Header("Face Center")]
    [HideInInspector]
    public bool startButtonPressed;
    private Vector3 direction;
    public GameObject centerLocation;
    public float moveSpeed;
    public float maxSpeed;
    public float faceSpeed;

    // Update is called once per frame
    void Update () {
        //Stops function from occuring if user presses start button
        if (startButtonPressed == false)
        {
            SpiralPosition();
        }
        //Move To Center
        else if (transform.GetComponent<Rigidbody>().useGravity == false)
        {
            //Look at Center
            Vector3 direction = centerLocation.transform.position - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, faceSpeed);

            //Direct Movement to Center
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 currentVelocity = rb.velocity;
            rb.AddForce(direction*moveSpeed, ForceMode.Impulse);
        }
        //Fall down tunnel
        else
        {
            return;
        }
	}

    private void SpiralPosition()
    {
        //Smooth Timer
        timer += Time.deltaTime;
        //Lerp Time
        float ping = Mathf.PingPong(timer * lerpTime, 1f);
        //Raises& drops height proportional to center Pos
        float height = Mathf.SmoothStep(0.1f, minHeight, ping);

        //Determins center Pos (distance from center)
        centeredPrct = Mathf.SmoothStep(circleWidth, 5f, ping);      
        
        //Spiral Movement
        Vector3 movement = new Vector3(-centeredPrct * Mathf.Sin(timer), height, (-centeredPrct * Mathf.Cos(timer)));
        transform.position = movement;

        //Face movement direction
        transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //Turns on Gravity
        if (other.name == "FallTrigger")
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
        }

        //Changes Scene
        if (other.name == "SceneChange")
        {
            buttonManager.GetComponent<MenuButtons>().PlayBtn("Tunnel");
        }
    }
}
