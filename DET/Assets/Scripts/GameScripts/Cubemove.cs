using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemove : MonoBehaviour
{
    Animator anim;
    public GameObject WinnweUi;
    public GameObject timerUi ;
    public GameObject HealthUi;
    public GameObject scoreUi;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collision detected");
            anim.SetBool("move", true);
            other.collider.transform.SetParent(transform);

        }
    }

    public void win()
    {
        WinnweUi.SetActive(true);
        scoreUi.SetActive(false);
        timerUi.SetActive(false);
        HealthUi.SetActive(false);


    }
}
