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

    public void LookAt(Vector3 point)
    {

        //here with the normal point the player is bending while looking at it so a new point which takes the players height in y axis is taken so the player doesn't bend while looking.
        Vector3 newPoint = new Vector3(point.x,transform.position.y,point.z);
        //Debug.Log(newPoint);
        transform.LookAt(newPoint);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
    }
}
