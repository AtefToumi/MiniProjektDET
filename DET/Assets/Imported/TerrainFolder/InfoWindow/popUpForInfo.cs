using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpForInfo : MonoBehaviour
{
    public GameObject popUpPanel;
    Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = popUpPanel.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
      {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("info");
           popUpPanel.SetActive(true);

            //Time.timeScale = 0f;
            
            Invoke(nameof(close), 5f);
            this.gameObject.SetActive(false);
       
        }
    }

     public void close()
     {

        //Time.timeScale = 1f;
        anim.SetTrigger("DisappearInfo");
        Invoke(nameof(wait), 1f);
        
         Debug.Log("close");


     }
    void wait()
    {
        popUpPanel.SetActive(false);
  
    }

}
