using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{   
    class Bullet{
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public int fireRate = 25;
    public int damage = 30;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem bloodSplash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public string weaponName;
    public WeaponRecoil recoil;
    public GameObject magazine;

    public int ammoCount;
    public int clipSize;
    
    
    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;

    private void Awake() {
        recoil = GetComponent<WeaponRecoil>();
    }



    Vector3 GetPosition(Bullet bullet){
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity){
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    // Start is called before the first frame update
    public void StartFiring(){
        isFiring = true;
        recoil.Reset();
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime){
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0.0f){
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }


    public void UpdateBullets(float deltaTime){
        SimulateBullets(deltaTime);
        DestroyBullets();
    }
    void DestroyBullets(){
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }

    void SimulateBullets(float deltaTime){
        bullets.ForEach(bullet => {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    } 

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet){
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if(Physics.Raycast(ray, out hitInfo)){
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 1.0f);

            if (hitInfo.collider.tag == "zombie")
                    {
                        Instantiate(bloodSplash, hitInfo.point, Quaternion.identity);
                    }
            ZombieHealth zombiehealth = hitInfo.transform.GetComponent<ZombieHealth>();
            if ( zombiehealth != null)
            {
                    zombiehealth.TakeDamage(damage, hitInfo.point);
                   
            }

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
        } else {
            bullet.tracer.transform.position = end;
        }
    }   
    public void FireBullet(){
        if(ammoCount <= 0){
            return;
        }
        ammoCount--;
        
        foreach(var particle in muzzleFlash){
            particle.Emit(1);
        }
        // play shootingSound 
        FindObjectOfType<AudioManager>().play("shootingSound");

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        recoil.GenerateRecoil(weaponName);

    }
    public void StopFiring() {
        isFiring = false;
    }
}
