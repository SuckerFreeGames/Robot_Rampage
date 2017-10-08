using UnityEngine;
using System.Collections;

public class BossBotBehavior : MonoBehaviour
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

    public GameObject[] waypoints;

    public int startingHealth = 100;
    public int currentHealth = 100;

    public int pointValueOnKill = 10;

    public ParticleSystem hitParticles;
    public ParticleSystem fire;
    public ParticleSystem fire2;

    public bool isDead = false;
    public bool sheilds = true;

    public enum State { scanning, tracking };

    public GameObject[] PowerModules;
    

    public State state;
    public GameObject target;
    public GameObject[] positionToLookFrom;
    public GameObject BossBot;
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
    public AudioClip scoreUp;

    public GameObject[] spawners;

    void Start()
    {

        hitParticles = GetComponent<ParticleSystem>();
       // currentTarget = waypoints[findIndexOfClosest()];
        state = State.scanning;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
 

        turnRight = randomBool();

    }


    void Update()
    {
        //print(PowerModules.Length);
        //if (PowerModules.Length <= -1) 
        if (PowerModules[1] == null && PowerModules[0] == null && PowerModules[2] == null && PowerModules[3] == null && PowerModules[4] == null && PowerModules[6] == null && PowerModules[5] == null && PowerModules[7] == null && PowerModules[8] == null && PowerModules[9] == null && PowerModules[10] == null && PowerModules[11] == null)
        {
        isDead = true;
        }

        if (isDead == true)
        {
            Vector3 position = transform.position += Vector3.up * 10;

            GameObject inst = (GameObject)Instantiate(explo, position, Quaternion.identity);
            GameObject inst2 = (GameObject)Instantiate(junk, position, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerBehaviorScript playerScript = player.GetComponent<PlayerBehaviorScript>();
            audio.PlayOneShot(hurt1, 0.3f);
            Destroy(gameObject);
            playerScript.score += pointValueOnKill;
            return;
        }

        if (state == State.scanning)
        {
            light.SetActive(false);
            light2.SetActive(false);
           // patrol();

            foreach (GameObject elem in spawners)
            {
                // print(elem);

                if (elem != null)
                {
                    elem.transform.Find("SpawnPortal").gameObject.SetActive(false);
                    elem.GetComponent<SpawnEnemyScript>().enabled = false;
                }
            }


           
            scanning();
        }
        else if (state == State.tracking)
        {
            light.SetActive(true);
            light2.SetActive(false);

           

            print(currentTarget);

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
                    fire.Play();
                    fire2.Play();
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
    /*
    int findIndexOfClosest()
    {
        float closestDistance = int.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, waypoints[i].transform.position) < closestDistance)
            {
                indexOfClosest = i;
                closestDistance = Vector3.Distance(this.transform.position, waypoints[i].transform.position);
            }
        }
        return indexOfClosest;
    }

    
    void patrol()
    {



        if (Vector3.Distance(this.transform.position, currentTarget.transform.position) <= navAgent.stoppingDistance)
        {
            if (waypoints.Length > indexOfClosest + 1)
            {
                indexOfClosest++;
                currentTarget = waypoints[indexOfClosest];
            }
            else
            {
                indexOfClosest = 0;

                currentTarget = waypoints[indexOfClosest];
            }
        }

        navAgent.SetDestination(currentTarget.transform.position);
    }
    */

    public void TakeDamage(int amount, Vector3 hitPoint)
    {



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
            if (BossBot.transform.localEulerAngles.y < RightExtreme)
            {
                turnRight = false;
            }

            BossBot.transform.Rotate(-Vector3.up * Time.deltaTime * speed);

        }
        else
        {
            if (BossBot.transform.localEulerAngles.y > LeftExtreme)
            {
                turnRight = true;
            }
            BossBot.transform.Rotate(Vector3.up * Time.deltaTime * speed);

        }

    }

    void lookAtTarget()
    {
     


        targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

    }



}

