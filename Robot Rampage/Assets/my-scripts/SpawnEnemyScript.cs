using UnityEngine;
using System.Collections;

public class SpawnEnemyScript : MonoBehaviour {

	public GameObject objectToSpawn;
	public float timeToWaitBetweenSpawns = 2.0f;
	private float timer = 0;
    public GameObject SpawnPortal;
    private ParticleSystem Portal;
    PlayerBehaviorScript behaviorScript;
    //public ParticleSystem[] SpawningPortal;
    
	// Use this for initialization
	void Start () {
		timer = 0;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
        behaviorScript = player.GetComponent<PlayerBehaviorScript>();

        
        Portal = SpawnPortal.GetComponent<ParticleSystem>();
        Portal.Play();



	}
	

	void Update () {
		if (behaviorScript.gameOver == true)
			return;
		
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {




			// reset the timer
            
			Instantiate(objectToSpawn, transform.position, transform.rotation);
            
			timer = 0;


		}
     
	}
}
