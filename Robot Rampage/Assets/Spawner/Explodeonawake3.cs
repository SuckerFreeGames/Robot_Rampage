using UnityEngine;
using System.Collections;

public class Explodeonawake3 : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}

    void Awake()
    {
        Debug.Log("Junk created.");

  
            Debug.Log("Fall.");
         GetComponent<Rigidbody>().useGravity = true;

        }
     
       

	}

