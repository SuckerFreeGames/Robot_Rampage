using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgent;
    public GameObject[] waypoints;
    Animator anim;
    private GameObject currentTarget;
    private int indexOfClosest;

    public enum State{patrol,pursue};

    public State state;
    public GameObject target;
    public GameObject[] positionToLookFrom;
    public float escapeDistance;
    public float attackRange;
    public GameObject beamlight;
    public ParticleSystem[] particlesofBeam;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentTarget = waypoints [findIndexOfClosest()];
        state = State.patrol;
        toggleBeam(false);
        navAgent.angularSpeed = 120;
        navAgent.speed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.patrol)
        {
            patrol();
        }
        else if (state == State.pursue)
        {
            pursue();
        }

        
    }
    

    int findIndexOfClosest()
    {
        float closestDistance = int.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if(Vector3.Distance(this.transform.position,waypoints[i].transform.position) < closestDistance) {
                indexOfClosest = i;
                closestDistance = Vector3.Distance(this.transform.position,waypoints[i].transform.position);
            }
        }
        return indexOfClosest;
    }

    void patrol() {

        if (scanForTarget())
        {

           // anim.SetTrigger("Walk");
            state = State.pursue;

        }

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

        navAgent.SetDestination (currentTarget.transform.position);
    }

    void pursue()
    {
        navAgent.SetDestination(target.transform.position);



        if (Vector3.Distance(this.transform.position, target.transform.position) > escapeDistance)
            {
                navAgent.angularSpeed = 120;
                navAgent.speed = 5;
                navAgent.acceleration = 8;
              state = State.patrol;


              currentTarget = waypoints[findIndexOfClosest()];
            }
        else if (Vector3.Distance(this.transform.position, target.transform.position) <= attackRange && anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
             
                anim.SetTrigger("Attack");
                navAgent.angularSpeed = 70;
                navAgent.speed = 5;
                navAgent.acceleration = 2;
                //toggle beam on
                toggleBeam(true);

            }

        else if (Vector3.Distance(this.transform.position, target.transform.position) <= attackRange && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                //toggle beam on
                toggleBeam(true);

            }
        else if (Vector3.Distance(this.transform.position, target.transform.position) > attackRange && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))

            {

                //toggle beam off
                toggleBeam(false);

            }

    }
    
  
    bool scanForTarget(){

        //CIRCULAR RANGE DETECTION
        //if(Vector3.Distance(this.transform.position, target.transform.position) < 30){
      //      state = State.pursue;
    //    }

        //ONE RAY DETECTION
/*        Ray[] raysForSearch = new Ray[1];
        Vector3 noAngle = positionToLookFrom.transform.forward;

        Debug.DrawLine(positionToLookFrom.transform.position, positionToLookFrom.transform.position + positionToLookFrom.transform.forward * 20);

        raysForSearch[0] = new Ray(positionToLookFrom.transform.position, noAngle);

        foreach (Ray r in raysForSearch)
        {
            RaycastHit hit;
            if (Physics.Raycast (r, out hit, 20))
            {
                if (hit.collider.transform.tag == "Player")
                {
                    return true;
                }
  */        
        //Multiple "sight" points Dectection
        for (int i = 0; i < positionToLookFrom.Length; i++) {
			Ray[] raysForSearch = new Ray[3];

			Vector3 noAngle = positionToLookFrom[i].transform.forward;
			Quaternion spreadAngle = Quaternion.AngleAxis (-20, new Vector3 (0, 1, 0));
			Vector3 negativeDirection = spreadAngle * noAngle;
			spreadAngle = Quaternion.AngleAxis (20, new Vector3 (0, 1, 0));
			Vector3 positiveDirection = spreadAngle * noAngle;

			Debug.DrawLine (positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + noAngle * 20);
			Debug.DrawLine (positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + positiveDirection * 20);
			Debug.DrawLine (positionToLookFrom[i].transform.position, positionToLookFrom[i].transform.position + negativeDirection * 20);


			raysForSearch [0] = new Ray (positionToLookFrom[i].transform.position, noAngle);
			raysForSearch [1] = new Ray (positionToLookFrom[i].transform.position, negativeDirection);
			raysForSearch [2] = new Ray (positionToLookFrom[i].transform.position, positiveDirection);

			foreach (Ray r in raysForSearch) {
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

    void toggleBeam(bool onState)
    {
        if (onState)
        {

            for (int i = 0; i < particlesofBeam.Length; i++)
            {
                particlesofBeam[i].Play();
                          
            }

     
               beamlight.SetActive(true);
              

        }

        else

        {
          
            for(int i = 0; i > particlesofBeam.Length; i++)
            {
                 particlesofBeam[i].Stop();

            }

            beamlight.SetActive(false);

        }

    }

}

