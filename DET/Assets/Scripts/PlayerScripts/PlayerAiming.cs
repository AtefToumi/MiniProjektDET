using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;




public class PlayerAiming : MonoBehaviour
{
    
    public float aimDuration = 0.3f;
    public float turnSpeed = 5;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    public Transform cameraLookAt;

    Camera mainCamera;
    Animator animator;
    ActiveWeapon activeWeapon;
    int isAimingParam = Animator.StringToHash("isAiming");
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    private void Update() {
        bool isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        var weapon = activeWeapon.GetActiveWeapon();
        if(weapon){
            weapon.recoil.recoilModifier = isAiming? 0.3f : 1.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
   
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    // void LateUpdate() {
    //     if(weapon){
    //         if(Input.GetButtonDown("Fire1")){
    //             weapon.StartFiring();
    //         }
    //         if(weapon.isFiring){
    //             weapon.UpdateFiring(Time.deltaTime);
    //         }
    //         weapon.UpdateBullets(Time.deltaTime);
    //         if(Input.GetButtonUp("Fire1")){
    //             weapon.StopFiring();
    //         }
    //     }
    // }
}