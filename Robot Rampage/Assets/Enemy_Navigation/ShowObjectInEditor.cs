using UnityEngine;
using System.Collections;

public class ShowObjectInEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmos()
    {
            // Draw a semitransparent blue cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, .5f);
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        
    }
}
