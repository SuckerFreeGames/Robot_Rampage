using UnityEngine;
using System.Collections;

public class BeamCollision : MonoBehaviour {
    public int attackDamage = 10;
    GameObject player;
    PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player") 
        {
            playerHealth.TakeDamage(attackDamage);
            print("hit player");
        }
    }
}
