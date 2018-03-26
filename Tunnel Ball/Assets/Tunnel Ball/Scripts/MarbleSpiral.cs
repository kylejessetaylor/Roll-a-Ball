using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpiral : MonoBehaviour {

    [Header("Position")]
    public float circleWidth = 9.25f;
    public float lerpTime = 0.2f;
    //Height
    public float minHeight = -1.47f;
    private float centeredPrct;
    private float timer;

	// Update is called once per frame
	void Update () {
        SpiralPosition();
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
}
