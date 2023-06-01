using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerShooter0601 : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint holdRig;
    [SerializeField] float reloadTime;

    WeaponHolder0601 weaponHolder;
    private Animator anim;
    private bool isReloading;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponHolder = GetComponentInChildren<WeaponHolder0601>();
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
