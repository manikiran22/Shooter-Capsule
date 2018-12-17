using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Vector3 _velocity;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
    }
}
