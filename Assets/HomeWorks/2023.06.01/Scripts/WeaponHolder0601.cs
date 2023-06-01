using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder0601 : MonoBehaviour
{
    [SerializeField] Gun0601 gun;

    public void Fire()
    {
        gun.Fire();
    }
}
