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
	AudioSource audio;
	public AudioClip spawnsfx;
    //public Vector3 SpawnPoint;

	// Use this for initialization
	void port () {
		audio = GetComponent<AudioSource> ();
		//timer = 0;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
        behaviorScript = player.GetComponent<PlayerBehaviorScript>();

        
        Portal = SpawnPortal.GetComponent<ParticleSystem>();
        Portal.Play();
       // SpawnPoint = SpawnPortal.transform.position;


	}

	void PlaySFX () {
		audio.PlayOneShot (spawnsfx, 0.70f);
	}

	void Update () {
        port();

		if (behaviorScript.gameOver == true)
			return;
		
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {




            spawnfix ();
			PlaySFX ();
            // reset the timer
            timer = 0;


		}

	}

    void spawnfix()
    {
        //int distance = Random.Range(4, 6);
        //Instantiate(objectToSpawn, SpawnPoint, Quaternion.identity);
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        //Vector3 position = transform.position += Vector3.forward * distance;
        // Instantiate(objectToSpawn, transform.position, transform.rotation);  //position, transform.rotation); 

    }
}
