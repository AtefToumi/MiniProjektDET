using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 7.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Animator anim;

    private Camera mainCamera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.Z) )
        {
            anim.SetBool("IsRunning",true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if(Input.GetKey(KeyCode.Q) )
        {
            anim.SetBool("Left",true);
        }
        else
        {
            anim.SetBool("Left", false);
        }

        if(Input.GetKey(KeyCode.S) )
        {
            anim.SetBool("Back",true);
        }
        else
        {
            anim.SetBool("Back", false);
        }

        if(Input.GetKey(KeyCode.D) )
        {
            anim.SetBool("Right",true);
        }
        else
        {
            anim.SetBool("Right", false);
        }
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLookAt = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z));
        }

    }
}