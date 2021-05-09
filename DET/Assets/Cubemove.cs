using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemove : MonoBehaviour
{
    Animator anim;
   // public GameObject player;
   // Transform transform;

    private void Start()
    {
        anim = GetComponent<Animator>();
      //  player.transform.position = GetComponent<Transform>().position; 
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collision detected");
            anim.SetBool("move", true);
            other.collider.transform.SetParent(transform);

            // player.transform.parent = other.gameObject.transform;
           // this.transform.position = player.transform.position;


           

        }
    }
}
