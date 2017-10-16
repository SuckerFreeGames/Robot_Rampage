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
//	AudioSource audio;
//	public AudioClip spawnsfx;
    
	// Use this for initialization
	void Start () {
	//	audio = GetComponent<AudioSource> ();
		timer = 0;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
        behaviorScript = player.GetComponent<PlayerBehaviorScript>();

        
        Portal = SpawnPortal.GetComponent<ParticleSystem>();
        Portal.Play();



	}

//	void PlaySFX () {
	//	audio.PlayOneShot (spawnsfx, 0.70f);
//	}

	void Update () {
		if (behaviorScript.gameOver == true)
			return;
		
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {




			// reset the timer
			//PlaySFX ();
			Instantiate(objectToSpawn, transform.position, transform.rotation);
			timer = 0;


		}

	}
}
