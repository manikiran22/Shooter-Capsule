using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]


public class Player : MonoBehaviour
{
    float moveSpeed = 10;

    Camera viewCamera;

    PlayerController controller;
    GunController gunController;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Update()
    {

        //MOVING INPUT
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        Vector3 velocity = input.normalized * moveSpeed;
        controller.Move(velocity);

        //LOOKAT INPUT
        //Creating a Ray that takes input from the camera to mouseposition
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //creating a groundplane which faces to the vector3 up direction
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        //creating the rayDistance from camera to the plane the ray touches
        float rayDistance;

        //raycast returns a bool if the ray touches the ground
        //Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the Scene.
        //here we are checking the condition if the ray is hitting the ground and once it returns true then cast the ray and see it touches the ground.
        if (groundPlane.Raycast(ray,out rayDistance))
        {

            //creating a point that hits the plane.
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.blue);

            //making the player look at the point.
            controller.LookAt(point);        
        }

        ShootInput();

    }

    //SHOOTING INPUT
    void ShootInput()
    {
        if (Input.GetMouseButton(0))
        {
            gunController.Shooter();
        }
    }

}
