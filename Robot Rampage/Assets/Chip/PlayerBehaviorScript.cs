using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Advertisements;

public class PlayerBehaviorScript : MonoBehaviour {
	


	[SerializeField] private float moveSpeed = 20.0f;
	[SerializeField] private LayerMask layerMask;
	private CharacterController characterController;
	private Vector3 currentLookTarget = Vector3.zero;



    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public float RotateSpeed = 0.25f;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public float speed = 3.0f;

	public GameObject gameOverPanel;
	PlayerHealth playerHealth;
	public int score;
    public Text scoreText;

    float playerSpeed = 10; //speed player moves




	Rigidbody playerRigidBody;
	bool isMoving = false;
	Animator anim;
	int floorMask;
	float camRayLength = 100.0f;
	public bool isEnabled = true;
	public bool gameOver = false;

	public AudioClip gameOverClip;
	public AudioSource audioSource;


	void Start () {
    //    Cursor.lockState = CursorLockMode.Locked;

		characterController = GetComponent<CharacterController> ();

        gameOverPanel.SetActive (false);
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		score = 0;
		scoreText.text = "Score: 0";
		audioSource = GetComponent<AudioSource> ();

		AdsSetup ();


	}
	
	// Update is called once per frame
	void Update () {

        //MoveForward();
    //    if (Input.GetKeyDown("m"))
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }

        if (playerHealth.currentHealth <= 0)
        {
			isEnabled = false;

			if (!gameOver)
            {
				Invoke ("DisplayGameOver", 1.0f);
                
            }
		}

		scoreText.text = "Score: " + score.ToString ();

	}

	void Awake() {
		playerRigidBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

	}

    public void FixedUpdate ()
	{
		if (isEnabled == false)
			return;
        

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Debug.DrawRay (ray.origin, ray.direction * 500, Color.blue);

		if(Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore)) 
		{
			if (hit.point != currentLookTarget) {
				currentLookTarget = hit.point;
			}

			Vector3 targetPosition = new Vector3 (hit.point.x, transform.position.y, hit.point.z);
			Quaternion rotation = Quaternion.LookRotation (targetPosition - transform.position);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * 10f);
		}
	

		Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));


		float translation = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;
		float straffe = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate(straffe, 0, translation);

		if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
		{
			anim.SetFloat("speed", -1);
		}

		else if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
		{
			anim.SetFloat("speed", 1);
		}

		else  if ((CrossPlatformInputManager.GetAxis("Horizontal") > 0))
		{
			anim.SetFloat("speed", 3);
		}

		else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
		{
			anim.SetFloat("speed", 2);
		}

		else
		{
			anim.SetFloat("speed", 0);
		}

	}




   




    public void DisableMovement()
	{
		isEnabled = false;
	}

	public void RestartGame(){
		SceneManager.LoadScene ("scene1");
	}

	void DisplayGameOver(){
		gameOver = true;
		gameOverPanel.SetActive (true);
		audioSource.PlayOneShot (gameOverClip);
		// todo: display ads
		//Advertisement.Show();
	}

	void AdsSetup(){
		//Advertisement.Initialize ("1041425", true);
	}

}
