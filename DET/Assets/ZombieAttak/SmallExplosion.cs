using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float radius;

    public float AfterTime;
    public ParticleSystem bloodStream;
    public ParticleSystem explision;


    public void Explosion()
    {
        Instantiate(explision, transform.position, Quaternion.identity);
        Instantiate(bloodStream, transform.position, Quaternion.identity);

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            
            Destroy(t.gameObject, AfterTime);
        }
    }
}
