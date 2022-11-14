using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField]
    private float           moveSpeed       = 2f;
    private Vector3         move            = Vector3.zero;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        move.Normalize();
        transform.position += move * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + move);

        animator.SetBool("isWalk", move != Vector3.zero);

        if (Input.GetKeyDown(KeyCode.Z))
            animator.SetTrigger("doAttack");
    }
}
