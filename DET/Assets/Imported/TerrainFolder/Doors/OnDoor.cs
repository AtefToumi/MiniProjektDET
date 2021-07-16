using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoor : MonoBehaviour
{
  //  bool isOpen = false;
    Animator anim;

    void Awake()
    {
        anim = this.transform.parent.GetComponent<Animator>();
        anim.SetBool("open", false);
   
    }


    public void onDoor()
    {
        anim.SetBool("open", true);
        anim.SetBool("close", false);
       
        //After 5 seconeden close the door 
       
        StartCoroutine(closeDoor());
       
    }

    IEnumerator closeDoor()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("open", false);
        anim.SetBool("close", true);
           
     
    }
}