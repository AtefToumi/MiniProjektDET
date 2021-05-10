using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    public int scoreOpenDoor;
    
 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(ScoreManager.score >= scoreOpenDoor)
        {
            animator.SetTrigger("DoorOpen");
        }
    }
}
