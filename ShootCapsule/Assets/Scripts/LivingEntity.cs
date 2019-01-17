using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageble
{

    public float startingHealth;
    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        startingHealth = health;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
    //first this class is created so the player,enemy share the same health status 
}
