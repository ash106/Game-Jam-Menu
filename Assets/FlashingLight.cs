using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {

	public Light flashingLight;
	public float threshold = 0.4f;

	private float randomNumber = 0;
	// Use this for initialization
	void Start () {
		flashingLight.intensity = 0;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		randomNumber = Random.value;
		if (randomNumber <= threshold) {
			flashingLight.intensity = 1.5f;
		} else {
			flashingLight.intensity = 1.0f;
		}
	}
}
