using UnityEngine;
using System.Collections;

public class Rescue_1 : MonoBehaviour {

	public RescueCounter RC;

	// Use this for initialization
	void Start () {
	
	}
	

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			RC.RCounter = RC.RCounter - 1;
			// destroy after collection
			Destroy (gameObject);
		}
	}
}
