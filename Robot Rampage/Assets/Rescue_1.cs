using UnityEngine;
using System.Collections;

public class Rescue_1 : MonoBehaviour {
	private UnityEngine.AI.NavMeshAgent navAgent;
	Animator anim;
	public RescueCounter RC;
	public GameObject target;
	float goingtotarget;
	bool saveonce;

	// Use this for initialization
	void Start () {
		goingtotarget = 5;
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		anim = this.GetComponent<Animator> ();
		anim.SetBool ("Saved", false);
		saveonce = false;
	}
	


	void OnTriggerEnter (Collider other)
	{
		if ( (other.gameObject.tag == "Player") && (saveonce == false)) {
			RC.RCounter = RC.RCounter - 1;
			anim.SetBool ("Saved", true);

			if (Vector3.Distance(this.transform.position, target.transform.position) > goingtotarget)
			{
				navAgent.SetDestination(target.transform.position);
				navAgent.angularSpeed = 120;
				navAgent.speed = 20;
				navAgent.acceleration = 8;
			}

			saveonce = true;

		}



	}
	void Update () {
		if (Vector3.Distance (this.transform.position, target.transform.position) <= goingtotarget) {

			saveonce = false;
			anim.SetBool ("Saved", false);
			Destroy (gameObject);
		}
	}
}
