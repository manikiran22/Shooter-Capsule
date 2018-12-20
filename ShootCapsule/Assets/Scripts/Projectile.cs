﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10;

    public LayerMask collisionMask;


    //this method is created so when ever we change the muzzle velocity it is reflected with the speed in this class.
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        //the distance the bullets are moving / and the ray is firing
        float moveDistance = speed * Time.deltaTime;
        
        //checking if the ray has hit something or not.
        CollisionCheck(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    //Creating a ray from projectile and checking if there was a hit
    void CollisionCheck(float moveDistance)
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitCheck(hit);
            Debug.DrawLine(ray.origin, Vector3.forward * moveDistance, Color.red);
        }
    }

    //here we are destroying the bullet and getting a return info of whether a bullet is hit or not.
    void OnHitCheck(RaycastHit hit)
    {
        //here first we are calling the IDamageble script 
        //and then on what ever with the hit HITS it is checking if the hitted object having the IDamageble so it can call the TakeHit method.
        //since TakeHit is the method that allows the projectile to destroy.
        IDamageble damageObject = hit.collider.GetComponent<IDamageble>();

        if (damageObject != null)
        {
            float damage = 1;
            damageObject.TakeHit(damage, hit);
        }
        //print(hit.collider.gameObject.name);

        GameObject.Destroy(gameObject);
    }
}
