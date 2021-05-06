using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(ScoreManager.score >= 100)
        {
            animator.SetTrigger("DoorOpen");
        }
    }
}
