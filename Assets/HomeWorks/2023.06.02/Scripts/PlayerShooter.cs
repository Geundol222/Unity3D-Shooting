using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace HomeWork0602
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] TwoBoneIKConstraint holdRig;
        [SerializeField] float reloadTime;

        WeaponHolder weaponHolder;
        private Animator anim;
        private bool isReloading;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            weaponHolder = GetComponentInChildren<WeaponHolder>();
        }

        private void OnReload(InputValue value)
        {
            if (isReloading)
                return;

            StartCoroutine(ReloadRoutine());
        }

        IEnumerator ReloadRoutine()
        {
            anim.SetTrigger("Reload");
            isReloading = true;
            holdRig.weight = 0f;
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;
            holdRig.weight = 1f;
        }

        public void Fire()
        {
            weaponHolder.Fire();
            anim.SetTrigger("Fire");
        }

        private void OnFire(InputValue value)
        {
            if (isReloading)
                return;
            Fire();
        }
    }
}