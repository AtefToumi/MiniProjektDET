/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject projectile;
   // public RotateToMouse
   // public GameObject FirePoit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            instantiateProjectile();
        }
    }

    void instantiateProjectile()
    {
        GameObject bullet;

     //   if ( FirePoit != null)
     //   {
            bullet = Instantiate(projectile, transform.position, Quaternion.identity);
     //   }

     //   else
     //   {
      //      Debug.Log("There is no Firepoint");
      //  }
    }
} */

using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables
    public float speed;
    public float maxDistance;

    private GameObject triggeringEnemy;
    public float damage;


    //Methods
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            maxDistance += 1 * Time.deltaTime;

            if (maxDistance >= 5)
            {
                Destroy(this.gameObject);
            }
        }
    }
}