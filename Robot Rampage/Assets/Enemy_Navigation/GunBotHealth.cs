using UnityEngine;
using System.Collections;

public class GunBotHealth : MonoBehaviour {
    public GameObject junk;
    public GameObject explode;
	public int startingHealth = 100;
	public int currentHealth = 100;

	public int pointValueOnKill = 10;

    //GameObject[] positionToLookFrom;

	bool isDead = false;
	Animator anim;
	UnityEngine.AI.NavMeshAgent agent;

	ParticleSystem hitParticles;

	AudioSource audio;
	public AudioClip hurt1;
	public AudioClip hurt2;
	public AudioClip hurt3;
	public AudioClip scoreUp;


	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		hitParticles = GetComponent<ParticleSystem> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void TakeDamage(int amount, Vector3 hitPoint){
		// we need to find out if the enemy is already or not
		if (isDead)
	
			return;

		currentHealth -= amount;
		//hitParticles.transform.position = hitPoint;

		hitParticles.Play ();
		if (currentHealth <= 0) {
			
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerBehaviorScript playerScript = player.GetComponent<PlayerBehaviorScript>();
			playerScript.score += pointValueOnKill;

			Death();
            
          
		}


	}

	public void Death(){
    
        int randomPick = Random.Range(1, 2);
        Vector3 position = transform.position += Vector3.up * randomPick; //new Vector3(randomPick, randomPick, randomPick);
        Vector3 position2 = transform.position;


	
        Rigidbody clone;
        GameObject inst = (GameObject)Instantiate(junk, position, Quaternion.identity);
        GameObject inst4 = (GameObject)Instantiate(explode, position, Quaternion.identity);
        Destroy(gameObject);
		isDead = true;



	}
		
}
