using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSmovement : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower;
    bool isJumping;

    Animator animator;

    Vector2 input;
    Vector3 rootMotion;
    Vector3 velocity;

    CharacterController cc;

    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
    }

    private void OnAnimatorMove(){
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate() {
        if(isJumping){ //Character in the air
            UpdateInAir();
        } else {    // Character on the ground
            UpdateOnGround();
        }
        
    }
    
    void UpdateOnGround(){
    Vector3 stepDownAmount = Vector3.down * stepDown;
    Vector3 stepForwardAmount = rootMotion * groundSpeed;
    cc.Move(stepForwardAmount + stepDownAmount);
    rootMotion = Vector3.zero;

    if (!cc.isGrounded){
        SetInAir(0);
        }
    }
    void UpdateInAir(){
    if(isJumping){ //Character in the air
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
        }
    }
    void Jump(){
    if(!isJumping){
        float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
        SetInAir(jumpVelocity);
        }
    }

    void SetInAir(float jumpVelocity){
    isJumping = true;
    velocity = animator.velocity * jumpDamp * groundSpeed;
    velocity.y = jumpVelocity;
    animator.SetBool("isJumping", true);
    }

    Vector3 CalculateAirControl(){
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

}