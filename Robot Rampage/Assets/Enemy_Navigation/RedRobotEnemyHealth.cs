using UnityEngine;
using System.Collections;

public class RedRobotEnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth = 100;

	public int pointValueOnKill = 10;

    //GameObject[] positionToLookFrom;

    public GameObject explo;
    public GameObject junk;
    //public GameObject sphere;

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
		
		if (isDead)
			return;

		currentHealth -= amount;
		//hitParticles.transform.position = hitPoint;
		hitParticles.Play ();
		if (currentHealth <= 0) {
			
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerBehaviorScript playerScript = player.GetComponent<PlayerBehaviorScript>();
			playerScript.score += pointValueOnKill;
			audio.PlayOneShot (scoreUp, 1.0f);
			Death();
            
          
		}

		int randomNumber = Random.Range (1, 3);
		switch (randomNumber) {
		case 1:
			audio.PlayOneShot (hurt1, 0.3f);
			break;

		case 2:
			audio.PlayOneShot (hurt2, 0.3f);
			break;

		case 3:
			audio.PlayOneShot (hurt3, 0.3f);
			break;

		}
	}

	public void Death(){
        Destroy(GetComponent<EnemyBehavior>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<MeshCollider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CharacterController>());

	
        //int randomPick = Random.Range(1, 2);

       
        Vector3 position = transform.position += Vector3.up * 4; //new Vector3(randomPick, randomPick, randomPick);
        Vector3 position2 = transform.position;

        GameObject inst = (GameObject)Instantiate(explo, position2, Quaternion.identity);
        GameObject inst2 = (GameObject)Instantiate(junk, position, Quaternion.identity);
        //GameObject inst3 = (GameObject)Instantiate(sphere, position, Quaternion.identity);
		
		isDead = true;

        Destroy(gameObject);


	}
		
}
