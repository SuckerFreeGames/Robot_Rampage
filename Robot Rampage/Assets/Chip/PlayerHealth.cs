﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float speed;
	public int startingHealth = 100;
	public int currentHealth = 100;
	public Text healthText;
	public Slider healthSlider;
    public float timer;


	float shakingTimer = 0;
	public float timeToShake = 1.0f;
	public float shakeIntensity = 3.0f;
	bool isShaking = false;

	bool isDead = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
		healthSlider.value = startingHealth;
	}

	// Update is called once per frame
	void Update () {
		//healthText.text = "HP: " + currentHealth.ToString ();
        timer += Time.deltaTime;
		healthText.text = "HP:";
		healthSlider.value = currentHealth;

		if (isShaking == true && shakingTimer < timeToShake) {
			shakingTimer += Time.deltaTime;
			float x = Mathf.PerlinNoise (Camera.main.transform.position.x, Camera.main.transform.position.y);
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + x * shakeIntensity, Camera.main.transform.position.y, Camera.main.transform.position.z);

			if (shakingTimer > timeToShake) {
				isShaking = false;
			}
		}

	}


	public void TakeDamage(int amount)
    {
		if (isDead)
			return;

		ShakeCamera ();

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}
	}

	public void Death(){
		if (isDead)
			return;
        //anim.SetFloat("speed", 4);
        timer = 0f;
		anim.SetTrigger ("die");
		//agent.enabled = false;
		isDead = true;
		currentHealth = 0;
  	//Destroy (gameObject, 2.5f);
        
	}

	void ShakeCamera(){
		shakingTimer = 0;
		isShaking = true;
		
	}
		
}
