using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject HitEffect;
    private Animator anim;
    private bool isHit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnReload(InputValue value)
    {
        anim.SetTrigger("Reload");
    }

    private void OnFire(InputValue value)
    {
        anim.SetTrigger("Fire");
    }
}
