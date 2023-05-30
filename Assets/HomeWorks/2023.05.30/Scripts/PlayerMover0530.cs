using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover0530 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    private Animator anim;
    private CharacterController controller;
    private Vector3 moveDir;
    private float ySpeed = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (GroundCheck() && ySpeed < 0)
            ySpeed = -1f;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);

        anim.SetFloat("Move", moveDir.z);
        anim.SetFloat("SideMove", moveDir.x);
    }

    private void OnJump(InputValue value)
    {
        if (GroundCheck())
            ySpeed = jumpSpeed;
    }

    private bool GroundCheck()
    {
        RaycastHit hit;

        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.6f);
    }
}
