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
        move.x = Input.GetAxisRaw("Horizontal");
        move.x += joystick.Horizontal;

        move.z = Input.GetAxisRaw("Vertical");
        move.z += joystick.Vertical;

        move.Normalize();
        transform.position += move * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + move);

        animator.SetBool("isWalk", move != Vector3.zero);

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("doAttack");
    }
}
