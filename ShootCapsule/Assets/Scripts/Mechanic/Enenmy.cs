﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enenmy : LivingEntity
{
    private Transform target;

    enum State { Chase, Idle, Attacking };
    State currentState;

    float attackThreshold = 1f;
    float timeBetAttack = 1;

    float nextAttackTime;

    Material skinMaterial;
    Color originalColor;

    float myCollisionRadius;
    float targetCollisionRadius;

    LivingEntity targetEntity;

    bool hasTarget;

    float damage = 2;

    NavMeshAgent agent;

   protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        //checking if the 
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {

            currentState = State.Chase;
            hasTarget = true;

            target = GameObject.Find("Player").GetComponent<Transform>();
            //another way of making player's transform as a target is and its transform specifically.
            //target = GameObject.FindGameObjectWithTag("Player").transform;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            //creating an entinty wrt the target
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;


            currentState = State.Chase;
            StartCoroutine(PathUpdate());
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {
        //the below will allow the enemy to go behind the player, but its costly on performance since multiple enemies will be implement.
        //to avoid that i have implemented coroutine since it can have a structured refresh rates.
        //agent.SetDestination(target.position);

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistTarget = (transform.position - target.position).sqrMagnitude;
                if (sqrDistTarget < Mathf.Pow(attackThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetAttack;
                    StartCoroutine(Attack());
                }
            }
        }

    }

    IEnumerator Attack()
    {
        
        currentState = State.Attacking;
        agent.enabled = false;     
        

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);



        /*Vector3 Temp = originalPosition;
        Temp.x += 5f;
        Temp.z += 5f;
        originalPosition = Temp;*/

        bool hasAppliedDamage = false;
        float percent = 0;

        float attackSpeed = 3;

        skinMaterial.color = Color.red;
        while (percent <= 1)
        {

            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                //targetEntity.TakeHit();
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            //  Debug.Log(percent);
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            

            yield return null;
        }

        skinMaterial.color = originalColor;
        currentState = State.Chase;
        agent.enabled = true;

    }

   IEnumerator PathUpdate()
    {
        float refreshRate = 0.25f;

        //while (target != null)
        while(hasTarget)
        {
            if (currentState == State.Chase)
            {

                //this is to find the normalized direction that is later used for getting the targetpos with an offset of the collision radius
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackThreshold / 2);

                //the below step can be done yet still yields the same result of tracking targets position
                //Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);


                //we did the if cause lets say player is dead then the below corotine will still run and since there will be no target it will throw out an error.
                if (!dead)
                {
                    agent.SetDestination(targetPos);

                }

            }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

