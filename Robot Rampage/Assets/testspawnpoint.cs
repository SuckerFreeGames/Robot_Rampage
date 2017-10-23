using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawnpoint : MonoBehaviour {

    public GameObject objectToSpawn;
    public float timeToWaitBetweenSpawns = 2.0f;
    private float timer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > timeToWaitBetweenSpawns)
        {
            
            Instantiate(objectToSpawn, position: transform.position, rotation: Quaternion.identity);

            // reset the timer
            timer = 0;


        }

    }
}
