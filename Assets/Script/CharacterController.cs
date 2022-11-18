using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float           moveSpeed       = 2f;
    private Vector3         move            = Vector3.zero;
    private Animator        animator;

    [SerializeField]
    private FixedJoystick joystick;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        GetInput();
        Locomotion();
        TryAttack();
    }


    private bool isAttck = false;
    private bool isDelay = false;
    private void GetInput()
    {
        // Locomotion
        move.x = Input.GetAxisRaw("Horizontal");
        move.x += joystick.Horizontal;
        move.z = Input.GetAxisRaw("Vertical");
        move.z += joystick.Vertical;

        // etc 
        isAttck = Input.GetKeyDown(KeyCode.Space);

    }
    private void Locomotion()
    {
        move.Normalize();

        if (isDelay)
            move = Vector3.zero;
        transform.position += move * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + move);
        animator.SetBool("isWalk", move != Vector3.zero);
    }

    private float attackTime = 0f;
    [SerializeField]
    private float attackRate = 1f;

    [SerializeField]
    private Weapon weapon;

    private void TryAttack()
    {
        isDelay = Time.time < attackTime;

        if (isAttck && !isDelay)
        {
            animator.SetTrigger("doAttack");
            weapon.Attack();
            attackTime = Time.time + attackRate;
        }
    }
}
