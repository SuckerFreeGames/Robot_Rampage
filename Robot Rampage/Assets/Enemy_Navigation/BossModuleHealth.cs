using UnityEngine;
using System.Collections;

public class BossModuleHealth : MonoBehaviour {

    bool D = false;
	public int startingHealth = 100;
	public int currentHealth = 100;
    public Vector3 postion;
	public int pointValueOnKill = 10;

    public GameObject junk;
    public GameObject junk2;
    public GameObject junk3;
    public GameObject junk4;

    public BossBotBehavior s;
   // EyeBotBehavior myScript;

   
	//Animator anim;
	//NavMeshAgent agent;

	ParticleSystem hitParticles;

	AudioSource audio;
	public AudioClip hurt1;
	public AudioClip hurt2;
	public AudioClip hurt3;
	public AudioClip scoreUp;

  

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;

       
        //s = gameObject.GetComponent<EyeBotBehavior>(); 
		//anim = GetComponent<Animator> ();
		//agent = GetComponent<NavMeshAgent> ();
		hitParticles = GetComponent<ParticleSystem> ();
		audio = GetComponent<AudioSource> ();
       //myScript = EyeBot.GetComponent<EyeBotBehavior>();


	}
	
	
	void Update () {
        
	}


	public void TakeDamage(int amount, Vector3 hitPoint){
		// Is object is destroyed or not
        if (D == true)
        {
            
            return;
        }

        if (D == false)
        {

            currentHealth -= amount;
            hitParticles.Play();
        }

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
  
         hitParticles.Play();
        
         /*Destroy(GetComponent<SpawnEnemyScript>());
         Destroy(GetComponent<CapsuleCollider>());
         Destroy(GetComponent<MeshCollider>());
         Destroy(GetComponent<Rigidbody>());
         Destroy(GetComponent<CharacterController>());
         Destroy(GetComponent<ObjectHealth>());
         */

        //anim.SetTrigger ("Death");


        print("Hub died");
		
        D = true;
        int randomPick = Random.Range(1, 2);

        Vector3 position = transform.position += Vector3.up * randomPick; //new Vector3(randomPick, randomPick, randomPick);
        Vector3 position2 = transform.position;
        Vector3 position3 = transform.position += Vector3.right * randomPick;

        Rigidbody clone;
        GameObject inst = (GameObject)Instantiate(junk, position, Quaternion.identity);
        GameObject inst2 = (GameObject)Instantiate(junk2, position2, Quaternion.identity);
        GameObject inst3 = (GameObject)Instantiate(junk3, position3, Quaternion.identity);
        GameObject inst4 = (GameObject)Instantiate(junk4, position, Quaternion.identity);
       // s.isDead = true;
        Destroy(gameObject);

        
	}
		
}
