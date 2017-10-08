using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

    //public GameObject[] trackedObjects;
    public List<GameObject> trackedObjects;
	public List<GameObject> radarObjects;
	public GameObject radarPrefab;
	public List<GameObject> borderObjects;
	public float switchDistance;
	public Transform helpTransform;
    static int removeto;

  

	// Use this for initialization
	void Start () {
		createRadarObjects();
	}
	

	void Update () {

        trackedObjects.RemoveAll(GameObject => GameObject == null);
        radarObjects.RemoveAll(GameObject => GameObject == null);
        borderObjects.RemoveAll(GameObject => GameObject == null);

        for (int i = 0; i < radarObjects.Count; i++)
        {

           
            if (Vector3.Distance(radarObjects[i].transform.position, transform.position) > switchDistance)
                {
                    //switch to borderObjects
                    helpTransform.LookAt(radarObjects[i].transform);
                    borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                    borderObjects[i].layer = LayerMask.NameToLayer("Radar");
                    radarObjects[i].layer = LayerMask.NameToLayer("Invisible");
                }
                else
                {
                    //switch back to radarObjects
                    borderObjects[i].layer = LayerMask.NameToLayer("Invisible");
                    radarObjects[i].layer = LayerMask.NameToLayer("Radar");
                }

            }
        
	}




	void createRadarObjects()
    {
		radarObjects = new List<GameObject>();
		borderObjects = new List<GameObject>();

		foreach(GameObject o in trackedObjects)
        {
			GameObject k = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            k.transform.transform.parent = o.transform;
            
            radarObjects.Add(k);

			GameObject j = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            j.transform.transform.parent = o.transform;
            borderObjects.Add(j);


        }
	}
}
