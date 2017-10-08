using UnityEngine;
using System.Collections;

public class Elevator_door_right : MonoBehaviour {

	public bool open = false;

	int HASGONE = 0;

	public Animation animatedDoor = null;

	// Use this for initialization
	void Start () {

		//		if (animatedDoor == null)
		//			Destroy(this); // The script needs the variable to be set

	}


	public void ChangeDoorState()
	{
		open = !open;
		DoorOpen ();
	}

	void DoorOpen() {

		if (open) {
			if (HASGONE == 1) 
			{
				animatedDoor.Play ("Elevator_door_right_open");
				HASGONE = 0;
			}
		}

		if (!open) {
			if (HASGONE == 0)
			{
				animatedDoor.Play ("Elevator_door_right");
				HASGONE = 1;
			}
		}

	}

	void Update ()
	{
		DoorOpen ();
	}

	/* 
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			animatedDoor.Play ("Elevator_door_right_open");
		}
	}

	void OnTriggerExit (Collider other)
	{

		if (other.tag == "Player") {
			animatedDoor.Play ("Elevator_door_right");			
		}


	}
	*/
}

