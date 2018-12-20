using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enenmy : LivingEntity
{
    public Transform target;

    NavMeshAgent agent;    

    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        //another way of making player's transform as a target is and its transform specifically.
        //target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(PathUpdate());

    }

   
    void Update()
    {
        //the below will allow the enemy to go behind the player, but its costly on performance since multiple enemies will be implement.
        //to avoid that i am thinking to use coroutine since it can have a structured refresh rates.
        //agent.SetDestination(target.position);


       
    }

    IEnumerator PathUpdate()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {

            //the below step can be done yet still yields the same result of tracking targets position
            //Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);

            agent.SetDestination(target.position);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
