using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun startingGun;

    public Transform weaponHold;

    Gun equippedGun;

    private void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    public void Shooter()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }

    //method that takes in a weapon/Gun object as an input.
    public void EquipGun(Gun gunToEquip)
    {

        //checking if there exists a gun already in the players hand
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        //since the equippedGun is destroyed we have to assign the new gun gunToEquip which is a param for the method
        //here the instantiation returns an object however the type of equipped gun is Gun 
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        //Weaponhold is the position at the players hand.
        //now what ever new gun is equipped has stay in the player hand while rotating or moving so for that we make it a child of player class.
        equippedGun.transform.parent = weaponHold;
    }
}
