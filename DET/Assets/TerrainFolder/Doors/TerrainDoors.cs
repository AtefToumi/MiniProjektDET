using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDoors : MonoBehaviour
{
    public float rayLength = 6f;
    public Transform rayBeginPoint;
    bool showAtTheFirstTime = true;
    
    public GameObject doorText;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        bool ishitted = Physics.Raycast(rayBeginPoint.position, rayBeginPoint.forward , out hitInfo, rayLength);

     
        if (ishitted)
        {
            StartCoroutine(showDoorText());
            Debug.Log("true");
            if(hitInfo.collider.gameObject.tag == "Door")
            {
                
                 
                Debug.Log("collider");
                if (Input.GetButton("Submit"))
                {
                    Debug.Log("enter");
                    hitInfo.collider.GetComponent<OnDoor>().onDoor();

                }
            }
           
        
        }
    }

    IEnumerator showDoorText()
    {
        if (showAtTheFirstTime)
        {
            doorText.SetActive(true);
           }
        yield return new WaitForSeconds(5f);
        doorText.SetActive(false);
        showAtTheFirstTime = false;
    }
}
