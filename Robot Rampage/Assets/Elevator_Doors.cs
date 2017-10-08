using UnityEngine;
using System.Collections;

public class Elevator_Doors : MonoBehaviour {

	public Elevator_door_right R;
	public Elevator_Door_Left L;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			L.open2 = true;
			R.open = true;
			//animatedDoor.Play ("Elevator_door_right_open");
			//this.transform.parent.animation.Play("Elevator_door_right_open");
		}
}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			L.open2 = false;
			R.open = false;
			//this.transform.parent.animation.Play("Elevator_door_right");
		}
	}
}
