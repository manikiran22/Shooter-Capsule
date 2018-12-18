using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Projectile projectile;

    public Transform muzzle;
    float muzzleVelocity = 35;
    float msBetweeenfire = 100;

    float nextBulletTime;

    public void Shoot()
    {
        if (Time.time > nextBulletTime)
        {
            nextBulletTime = Time.time + msBetweeenfire / 1000;
            Debug.Log(nextBulletTime);
        }

        Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
        newProjectile.SetSpeed(muzzleVelocity);
    }
 
}
