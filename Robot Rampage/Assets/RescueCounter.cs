using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Advertisements;

public class RescueCounter : MonoBehaviour {

	public int RCounter = 10;
	public Text RCtext;

	// Use this for initialization
	void Start () {
		RCounter = 10;
		RCtext.text = "Survivors Left: 10";
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		RCtext.text = "Survivors: " + RCounter.ToString ();

		if (RCounter <= 0) {
//			Application.LoadLevel("Title_Level");
			RCtext.text = "Escape to the Elevator!";
		}
	}


}
