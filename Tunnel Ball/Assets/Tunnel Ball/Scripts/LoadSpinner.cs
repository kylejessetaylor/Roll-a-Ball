using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpinner : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageRotate;
    public float rotateSpeed = 200f;

    // Use this for initialization
    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageRotate = rectComponent.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        rectComponent.Rotate(0f, 0f, -(rotateSpeed * Time.unscaledDeltaTime));
    }
}
