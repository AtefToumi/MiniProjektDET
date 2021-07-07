using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerAiming : MonoBehaviour
{
    RaycastWeapon weapon;
    public float turnSpeed = 5;
    Camera mainCamera;
    public float aimDuration = 0.3f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate() {
        if(weapon){
            if(Input.GetButtonDown("Fire1")){
                weapon.StartFiring();
            }
            if(weapon.isFiring){
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullets(Time.deltaTime);
            if(Input.GetButtonUp("Fire1")){
                weapon.StopFiring();
            }
        }
    }
}