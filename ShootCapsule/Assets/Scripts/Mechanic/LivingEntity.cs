using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageble
{

    public float startingHealth;
    protected float health;
    protected bool dead;

    //creating an event so the enemy can sybscribe to it and destroy the GO on death.
    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        //do something witht he hit variable
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {

        print(damage);

        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    public void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();            
        }

        GameObject.Destroy(gameObject);
    }
    //first this class is created so the player,enemy share the same health status 
} 
