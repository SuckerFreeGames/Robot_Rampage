using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using DigitalRuby.PyroParticles;

public class PlayerShoot : MonoBehaviour {


	RaycastHit shootHit;
	Ray shootRay;
	//LineRenderer laserLine;
	int shootableMask;
    bool isShooting = false;
	//GameObject LaserBeamOrigin;
	//GameObject LaserBeamEnd;
	public Light spotLight;
	public int damagePoints = 10;

	public bool isEnabled = true;

    public GameObject[] projectilePrefabs;
    private GameObject selectedProjectilePrefab;
    private GameObject currentPrefabObject;
    FireBaseScript currentPrefabScript;
    public GameObject projectileSpawnPoint;

    /*
	public AudioClip laser1;
	public AudioClip laser2;
	public AudioClip laser3;
	public AudioClip laser4;
    
	AudioSource audio;
    */


    // Use this for initialization
	void Start () {
		//shootableMask = LayerMask.GetMask ("Enemies");
		//laserLine = GetComponentInChildren<LineRenderer> ();
		//LaserBeamOrigin = GameObject.FindGameObjectWithTag ("LaserBeamOrigin");
		//LaserBeamEnd = GameObject.FindGameObjectWithTag ("LaserBeamEnd");
		spotLight.enabled = false;
		//laserLine.enabled = false;
		//audio = GetComponent<AudioSource> ();

        InitializeProjectile ();
	}
	
	// Update is called once per frame
	void Update () {
		//laserLine.SetPosition(0, LaserBeamOrigin.transform.position);

		#if !MOBILE_INPUT
		if (Input.GetButtonDown ("Fire1") && isShooting == false && isEnabled == true) {
			Shoot ();
			
		} 
		#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0)
        {
			Shoot();
		}

#endif

    }


	public void Shoot(){
		isShooting = true;

        SpawnProjectile();


		spotLight.enabled = true;


		/*laserLine.enabled = true;

		laserLine.SetPosition(0, LaserBeamOrigin.transform.position);


	
		shootRay.origin = LaserBeamOrigin.transform.position;
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHit, 200.0f, shootableMask)) {
			laserLine.SetPosition (1, shootHit.point);
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
            EyeBotBehavior eyeBotBehavior = shootHit.collider.GetComponent<EyeBotBehavior>();
            ObjectHealth objectHealth = shootHit.collider.GetComponent<ObjectHealth>();
            PowerModuleHealth powerModuleHealth = shootHit.collider.GetComponent<PowerModuleHealth>();
            GunBotHealth gunBotHealth = shootHit.collider.GetComponent<GunBotHealth>();
            RedRobotEnemyHealth redRobotEnemyHealth = shootHit.collider.GetComponent<RedRobotEnemyHealth>();
            BossModuleHealth bossModuleHealth = shootHit.collider.GetComponent<BossModuleHealth>();
            BossBotBehavior bossBotBehavior = shootHit.collider.GetComponent<BossBotBehavior>();


            if (objectHealth !=null)
            {
                objectHealth.TakeDamage(damagePoints, shootHit.point);
            }


            if (powerModuleHealth != null)
            {
                powerModuleHealth.TakeDamage(damagePoints, shootHit.point);
            }

            if (bossModuleHealth != null)
            {
                bossModuleHealth.TakeDamage(damagePoints, shootHit.point);
            }

            if (bossBotBehavior != null)
            {
                bossBotBehavior.TakeDamage(damagePoints, shootHit.point);
            }

            if (gunBotHealth != null)
            {
                gunBotHealth.TakeDamage(damagePoints, shootHit.point);
            }

            if (redRobotEnemyHealth != null)
            {
                redRobotEnemyHealth.TakeDamage(damagePoints, shootHit.point);
            }

            if (eyeBotBehavior != null)
            {
                eyeBotBehavior.TakeDamage(damagePoints, shootHit.point);
            }

            else if (enemyHealth != null) {
				enemyHealth.TakeDamage (damagePoints, shootHit.point);
			}
		} else {
			laserLine.SetPosition (1, LaserBeamEnd.transform.position);
		}

      */

		Invoke ("StopShooting", 0.15f);

        /*
         
        int randomNumber = Random.Range (1, 4);
		switch (randomNumber) {
		case 1:
			audio.PlayOneShot (laser1);
			break;

		case 2:
			audio.PlayOneShot (laser2);
			break;

		case 3:
			audio.PlayOneShot (laser3);
			break;

		case 4:
			audio.PlayOneShot (laser4);
			break;
			
         */
		}



	void StopShooting(){
		//laserLine.enabled = false;
		isShooting = false;
		spotLight.enabled = false;
	}

	public void DisableShooting(){
		isEnabled = false;
	}

    void InitializeProjectile()
    {
        int selected = Random.Range(1, 10000) % projectilePrefabs.Length;
        selectedProjectilePrefab = projectilePrefabs[selected];


    }

void SpawnProjectile() {
    currentPrefabObject = GameObject.Instantiate(selectedProjectilePrefab);
    currentPrefabObject.transform.position = projectileSpawnPoint.transform.position;
    currentPrefabObject.transform.rotation = transform.rotation;


}
}
