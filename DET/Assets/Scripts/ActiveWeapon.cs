using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot{
        Primary = 0,
        Secondary = 1
    }
    public Transform crossHairTarget;
    public Transform[] weaponSlots;
    RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    public PlayerAiming playerAiming;
    public AmmoWidget ammoWidget;

    int activeWeaponsIndex;
    public Animator rigController;
    bool isHolstered = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon){
            Equip(existingWeapon);
        }
    }
    

    public RaycastWeapon GetActiveWeapon(){
        return GetWeapon(activeWeaponsIndex);
    }

    RaycastWeapon GetWeapon(int index){
        if(index < 0 || index >= equipped_weapons.Length){
            return null;
        }
        return equipped_weapons[index];
    }

    

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponsIndex);
        if(weapon && !isHolstered)
        {
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
        if(Input.GetKeyDown(KeyCode.F)){
            ToggleActiveWeapon();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon){
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if(weapon){
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.recoil.playerAiming = playerAiming;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equipped_weapons[weaponSlotIndex] = weapon;  

        SetActiveWeapon(newWeapon.weaponSlot);
        ammoWidget.RefreshAmmo(weapon.ammoCount, (int)weapon.weaponSlot);
        ammoWidget.RefreshClip(weapon.clipCount, (int)weapon.weaponSlot);
    }

    void ToggleActiveWeapon(){
        bool isHolstered = rigController.GetBool("holster_weapon");
        if(isHolstered){
            StartCoroutine(ActivateWeapon(activeWeaponsIndex));
        } else {
            StartCoroutine(HolsterWeapon(activeWeaponsIndex));
        }
    }
    void SetActiveWeapon(WeaponSlot weaponSlot){
        int holsterIndex = activeWeaponsIndex;
        int activateIndex = (int) weaponSlot;

        if(holsterIndex == activateIndex){
            holsterIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holsterIndex,activateIndex));
    }
    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex){
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponsIndex = activateIndex;
    }
    IEnumerator HolsterWeapon(int index){
        isHolstered = true;
          var weapon = GetWeapon(index);
          if(weapon){
              rigController.SetBool("holster_weapon", true);
              do {
                  yield return new WaitForEndOfFrame();
              } while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
          }
    }
    IEnumerator ActivateWeapon(int index){
          var weapon = GetWeapon(index);
          if(weapon){
            ammoWidget.RefreshAmmo(weapon.ammoCount, (int)weapon.weaponSlot);
            ammoWidget.RefreshClip(weapon.clipCount, (int)weapon.weaponSlot);
            rigController.SetBool("holster_weapon", false);
              rigController.Play("equip_" + weapon.weaponName);
              do {
                  yield return new WaitForEndOfFrame();
              } while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
          }
          isHolstered = false;
    }

    public void RefillAmmo(int clipCount)
    {
        var weapon = GetActiveWeapon();
        if (weapon)
        {
            weapon.clipCount += clipCount;
            ammoWidget.RefreshAmmo(weapon.ammoCount, (int)weapon.weaponSlot);
            ammoWidget.RefreshClip(weapon.clipCount, (int)weapon.weaponSlot);
        }

    }
}
