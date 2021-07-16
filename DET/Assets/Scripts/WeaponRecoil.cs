using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public PlayerAiming playerAiming;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;

    public Vector2[] recoilPattern;
    float horizontalRecoil;
    float verticalRecoil;
    public float duration;
    public float recoilModifier = 1;

    float time;
    int index;
    
    private void Awake() {
        cameraShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }
    int NextIndex(int index){
        return (index + 1) % recoilPattern.Length;
    }
    public void Reset() {
        index = 0;
    }

    public void GenerateRecoil(string weaponName){
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex(index);

        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }
    
    void Update()
    {
        if(time> 0){
            playerAiming.yAxis.Value -= (((verticalRecoil/10) * Time.deltaTime) /duration) * recoilModifier;
            playerAiming.xAxis.Value -= (((horizontalRecoil/10) * Time.deltaTime) /duration) * recoilModifier;
            time -= Time.deltaTime;
        }
        
    }
}
