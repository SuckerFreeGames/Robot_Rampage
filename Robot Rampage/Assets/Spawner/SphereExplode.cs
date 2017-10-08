using UnityEngine;
using System.Collections;

public class SphereExplode : MonoBehaviour {

    private float radius = 0.25F;
    private float power = 0.05F;
    
    void Start() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) { }
              //  rb.AddExplosionForce(power, explosionPos, radius, 1F, ForceMode.Impulse);

         

}

}
    void Update()
    {
        Destroy(gameObject);
    }
}
    
