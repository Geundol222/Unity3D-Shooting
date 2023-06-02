using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork0602
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] Gun gun;

        public void Fire()
        {
            gun.Fire();
        }
    }
}