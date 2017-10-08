using UnityEngine;
using System.Collections;

public class Explodeonawake : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {
                Debug.Log("Fall.");
        //GetComponent<Rigidbody>().useGravity = true;
    }

    void Awake()
    {
        Debug.Log("Junk created.");




    }



}


