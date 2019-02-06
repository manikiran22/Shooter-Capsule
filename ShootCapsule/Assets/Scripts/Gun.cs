using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Projectile projectile;

    public Transform muzzle;
    public float muzzleVelocity = 35;
    public float msBetweeenfire = 100;

    float nextBulletTime;
    Vector3 offsetDistance;

    private void OnEnable()
    {
        offsetDistance = new Vector3(0, 0, 0.2f);
    }

    public void Shoot()
    {
        if (Time.time > nextBulletTime)
        {
            nextBulletTime = Time.time + msBetweeenfire / 1000;
           // Debug.Log(nextBulletTime);
        }

        //though there is projectile for Projectile we are creating the newProjectile so the projectile can be instantiated.
        Projectile newProjectile = Instantiate(projectile, muzzle.position + offsetDistance, muzzle.rotation) as Projectile;
        newProjectile.SetSpeed(muzzleVelocity);
    }
 
}
