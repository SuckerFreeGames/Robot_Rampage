using UnityEngine;
using System.Collections;

public class EyeBotBehavior : MonoBehaviour
{
    public float timer = 0f;
    public float delay = 1f;
    private bool turnRight;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private GameObject currentTarget;
    public Vector3 postion;
    private int indexOfClosest;

    public GameObject light;
    public GameObject light2;

    public GameObject explo;
    public GameObject junk;

    //public GameObject shoot;

    public int startingHealth = 100;
    public int currentHealth = 100;

    public int pointValueOnKill = 10;

    public ParticleSystem hitParticles;
    public ParticleSystem fire;

    public bool isDead = false;
    public bool sheilds = true;

    public enum State { scanning, tracking };

    public State state;
    public GameObject target;
    public GameObject[] positionToLookFrom;
    public GameObject EyeBot;
    public float escapeDistance;
    public float attackRange;
    public float RightExtreme;
    public float LeftExtreme;
    private Quaternion targetRotation;
    public float speed;

    private Vector3 targetPoint;

    AudioSource audio;
    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip hurt3;
	public AudioClip laser;
    public AudioClip scoreUp;

    public GameObject[] spawners;

    void Start()
    {

        hitParticles = GetComponent<ParticleSystem> ();

        state = State.scanning;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();

        turnRight = randomBool ();

    }


    void Update()
    {

	

        if (isDead)
        {

            Vector3 position = transform.position += Vector3.up * 10;

			GameObject inst = (GameObject)Instantiate(explo, position, Quaternion.identity);
			GameObject inst2 = (GameObject)Instantiate(junk, position, Quaternion.identity);
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			PlayerBehaviorScript playerScript = player.GetComponent<PlayerBehaviorScript>();

			Destroy(gameObject);
			playerScript.score += pointValueOnKill;
            return;
        }

        if (state == State.scanning)
        {
            light.SetActive(false);
            light2.SetActive(false);
            foreach (GameObject elem in spawners)
            {
               // print(elem);

                if (elem != null)
                {
                    elem.transform.Find(name: "SpawnPortal").gameObject.SetActive(false);
                    elem.GetComponent<SpawnEnemyScript>().enabled = false;
                }
            }

            scanning();
        }
        else if (state == State.tracking)
        {
            light.SetActive(true); 
            light2.SetActive(false);

            if (state == State.tracking)
            {
                
                timer += Time.deltaTime * 1;
                
                if (timer >= delay / 2)
                {
                    light2.SetActive(true);

                }

                if (timer >= delay)
                {
                    light2.SetActive(true);
					audio.PlayOneShot(laser, 0.3f);
                    fire.Play();
                    //GameObject inst4 = (GameObject)Instantiate(fire, transform.position, Quaternion.identity);
                    timer = 0;
                }

                foreach (GameObject elem in spawners)
                    if (elem != null)
                    {
                        {
                            elem.transform.Find("SpawnPortal").gameObject.SetActive(true);
                            elem.GetComponent<SpawnEnemyScript>().enabled = true;
                            

                        }
                    }

                //GameObject.Find("SPAWNER").GetComponent<SpawnEnemyScript>().enabled = true;
                //gameObject.GetComponent<SpawnEnemyScript>().enabled = true;
                tracking();

            }
        }
     }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {


		audio.PlayOneShot(hurt1, 0.5f);
        hitParticles.Play();

      


        }
    


    void scanning()
    {
        turnEye();
        if (scanForTarget())
        {
         
            state = State.tracking;

        }



    }

    void tracking()
    {




        if (Vector3.Distance(this.transform.position, target.transform.position) > escapeDistance)
        {
			audio.PlayOneShot(hurt3, 1f);
            state = State.scanning;

        }

        else if (Vector3.Distance(this.transform.position, target.transform.position) <= attackRange)
        {

            lookAtTarget();


        }


    }


    bool scanForTarget()
    {



        for (int i = 0; i < positionToLookFrom.Length; i++)
        {
            Ray[] raysForSearch = new Ray[3];

            Vector3 noAngle = positionToLookFrom[i].transform.forward;
            Quaternion spreadAngle = Quaternion.AngleAxis(-20, new Vector3(0, 1, 0));
            Vector3 negativeDirection = spreadAngle * noAngle;
            spreadAngle = Quaternion.AngleAxis(20, new Vector3(0, 1, 0));
            Vector3 positiveDirection = spreadAngle * noAngle;

            Debug.DrawLine(positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + noAngle * 32);
            Debug.DrawLine(positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + positiveDirection * 32);
            Debug.DrawLine(positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + negativeDirection * 32);


            raysForSearch[0] = new Ray(positionToLookFrom[i].transform.position, noAngle);
            raysForSearch[1] = new Ray(positionToLookFrom[i].transform.position, negativeDirection);
            raysForSearch[2] = new Ray(positionToLookFrom[i].transform.position, positiveDirection);

            foreach (Ray r in raysForSearch)
            {
                RaycastHit hit;
                if (Physics.Raycast(r, out hit, 20))
                {
                    if (hit.transform.tag == "Player")
                    {
						audio.PlayOneShot(hurt2, 1f);
                        return true;
                    }

                }
            }
        }
        return false;
    }

   
    bool randomBool()
    {
        if (Random.value > 0.5)
        {
            return true;
        }
        return false;
    }

    void turnEye()
    {

        if (turnRight)
        {
            if (EyeBot.transform.localEulerAngles.y < RightExtreme)
            {
                turnRight = false;
            }

            EyeBot.transform.Rotate(-Vector3.up * Time.deltaTime * speed);

        }
        else
        {
            if (EyeBot.transform.localEulerAngles.y > LeftExtreme)
            {
                turnRight = true;
            }
            EyeBot.transform.Rotate(Vector3.up * Time.deltaTime * speed);

        }

    }

    void lookAtTarget()
    {
        /*
        //Vector3 heightAdjustedTarget = new Vector3(target.transform.position.x, EyeBot.transform.position.y, target.transform.position.z);
        //Vector3 directionToTarget = heightAdjustedTarget - EyeBot.transform.position;//target.transform.up - EyeBot.transform.up;
       
        Vector3 directionToTarget = target.transform.up - EyeBot.transform.up;
        Vector3 EyeBotPosition = EyeBot.transform.up;

        Debug.Log("directionToTarget = " + directionToTarget);
        Debug.Log("EyeBotPosition = " + EyeBotPosition);

        if (Vector3.Dot(directionToTarget, EyeBotPosition) > 0)
        {
            EyeBot.transform.Rotate(-Vector3.up);
        }
        else if (Vector3.Dot(directionToTarget, EyeBot.transform.position) <= -1)
        {
            EyeBot.transform.Rotate(Vector3.up);
        }
        */

	
            targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        
    }


    
}

