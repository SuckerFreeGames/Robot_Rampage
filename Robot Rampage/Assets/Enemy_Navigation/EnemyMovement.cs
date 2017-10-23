using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	GameObject player;
	UnityEngine.AI.NavMeshAgent nav;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();

    }
	
	// Update is called once per frame
	void Update () {
        if (nav.enabled == false)
        {
            return;
        }
        else
        {
            setnavdest();
        }
    }

    private void setnavdest()
    {
        nav.SetDestination(player.transform.position);
    }
}
