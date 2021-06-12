using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    // public Transform target;
    Transform target;

    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    GameObject player;

    bool RotateAroundPlayer = true ;
    float rotationsSpeed = 5.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        offset = transform.position - target.position;
    }
    
    void LateUpdate()
    {
        if (RotateAroundPlayer)        //
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationsSpeed, Vector3.up);  //
            offset = camTurnAngle * offset ;       //
        }  

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition; 
        target = player.transform;

        if ( RotateAroundPlayer)    //
        {
            transform.LookAt(target);
        }
    }

   
} 


