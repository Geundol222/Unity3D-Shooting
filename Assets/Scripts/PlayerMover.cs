using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;

    private Animator anim;
    private CharacterController controller;
    private Vector3 moveDir;
    private float moveSpeed;
    private float ySpeed = 0f;
    private bool isGrounded;
    private bool isWalking;

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

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void Move()
    {
        // 월드 기준 움직임
        // controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 로컬기준 움직임
        if (moveDir.magnitude == 0)     // 안움직임
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);
        }
        else if (isWalking)             // 걷고 있을 때
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        }
        else                            // 뛰고 있을 때
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);
        }

        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        anim.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        anim.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
        anim.SetFloat("Speed", moveSpeed);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (isGrounded && ySpeed < 0)
            ySpeed = -1f;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnJump(InputValue value)
    {
        if (isGrounded)
            ySpeed = jumpSpeed;
    }

    private void OnWalk(InputValue value)
    {
        isWalking = value.isPressed;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.6f);
    }
}
