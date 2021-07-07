using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig handIk;
    public Transform weaponParent;
    public Transform WeaponLeftGrip;
    public Transform WeaponRightGrip;
    RaycastWeapon weapon;
    public Animator rigController;
    

    // Start is called before the first frame update
    void Start()
    {
        
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon){
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon)
        {
            if(Input.GetButtonDown("Fire1")){
                weapon.StartFiring();
            }
            if(weapon.isFiring) {
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullets(Time.deltaTime);
            if(Input.GetButtonUp("Fire1")){
                weapon.StopFiring();
            }
            if(Input.GetKeyDown(KeyCode.F)){
                bool isHolstered = rigController.GetBool("holster_weapon");
                rigController.SetBool("holster_weapon", !isHolstered);
            }
        }
        
    }

    public void Equip(RaycastWeapon newWeapon){
        if(weapon){
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + weapon.weaponName);
        
    }
}
