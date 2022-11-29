using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharBase
{
    public void TakeDamage(int damage);
    public void TakeStun(float time);
}

public class UnitState
{
    public int currentHP;
    public int maxHP;
    public int defence;
    public int attackDamage;
    public float attackRange;
    public float attackRate;
    public float moveSpeed;

    public int CalculateDamage(int takeDamage)
    {
        int result = takeDamage - defence;
        return result > 0 ? result : 1;
    }
}


public class CharacterController : MonoBehaviour, ICharBase
{
    [SerializeField]
    private float           moveSpeed       = 2f;
    private Vector3         move            = Vector3.zero;
    private Animator        animator;

    private FixedJoystick joystick;

    private UnitState state = new UnitState();

    private Rigidbody rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        state.currentHP = 10;
        state.defence = 2;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        rb.velocity = Vector3.zero;
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

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " 공격 받았습니다 데미지는 " + damage);
        //여기다가 기능구현
    }

    public void TakeStun(float time)
    {
        Debug.Log(gameObject.name + " 스턴을 당했습니다 시간은 " + time);
    }
}
