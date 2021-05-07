using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    public int Score = 100;
 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(ScoreManager.score >= Score)
        {
            animator.SetTrigger("DoorOpen");
        }
    }
}
