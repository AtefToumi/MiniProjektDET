using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Cinemachine.CinemachineFreeLook playerCamera;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;

    public Vector2[] recoilPattern;
    float horizontalRecoil;
    float verticalRecoil;
    public float duration;

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

    public void GenerateRecoil(){
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex(index);
    }
    
    void Update()
    {
        if(time> 0){
            playerCamera.m_YAxis.Value -= ((verticalRecoil/1000) * Time.deltaTime) /duration;
            playerCamera.m_XAxis.Value -= ((horizontalRecoil/10) * Time.deltaTime) /duration;
            time -= Time.deltaTime;
        }
        
    }
}
