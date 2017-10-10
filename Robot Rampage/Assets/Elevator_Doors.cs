using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Elevator_Doors : MonoBehaviour {

	public Elevator_door_right R;
	public Elevator_Door_Left L;

	RescueCounter C;

	// Use this for initialization
	void Start () {
		GameObject COUNTER = GameObject.FindGameObjectWithTag ("Counter");
		C = COUNTER.GetComponent<RescueCounter> ();
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (C.RCounter);

	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			L.open2 = true;
			R.open = true;
			//animatedDoor.Play ("Elevator_door_right_open");
			//this.transform.parent.animation.Play("Elevator_door_right_open");

			if (C.RCounter == 0) {

				Application.LoadLevel("Title_Level");
			}

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
