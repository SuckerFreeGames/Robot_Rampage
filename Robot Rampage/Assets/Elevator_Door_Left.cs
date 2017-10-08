using UnityEngine;
using System.Collections;

public class Elevator_Door_Left : MonoBehaviour 
{

	public bool open2 = false;

	int HASGONE = 0;

	public Animation animatedDoor = null;

	// Use this for initialization
	void Start () {
		
//		if (animatedDoor == null)
//			Destroy(this); // The script needs the variable to be set
	
	}


	public void ChangeDoorState()
	{
		open2 = !open2;
		DoorOpen ();
	}

	void DoorOpen() {

		if (open2) {
			if (HASGONE == 1) 
			{
				animatedDoor.Play ("Elevator_door_left_open");
				HASGONE = 0;
			}
		}

		if (!open2) {
			if (HASGONE == 0)
			{
				animatedDoor.Play ("Elevator_door_left");
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
			animatedDoor.Play ("Elevator_door_left_open");
		}
	}

	void OnTriggerExit (Collider other)
	{

		if (other.tag == "Player") {
			animatedDoor.Play ("Elevator_door_left");			
		}
	

	}
	*/
}
