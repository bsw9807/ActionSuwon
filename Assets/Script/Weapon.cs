using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {  Melee, Range };

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private AttackType type;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private Vector3 attackArea;
    [SerializeField]
    private TrailRenderer trail;
    private BoxCollider attackCollider;


    private void Awake()
    {
        InitWeapon();
    }
    public void InitWeapon()
    {
        if(attackCollider == null)
            attackCollider = GetComponent<BoxCollider>();
        attackCollider.size = attackArea;
        attackCollider.center = new Vector3(0f, -attackArea.y / 2, 0f);
    }

    public void Attack()
    {
        switch(type)
        {
            case AttackType.Melee:
                MeleeAtaack();
                break;
                
            case AttackType.Range:
                break;
        }
    }

    private Coroutine coroutine;
    private void MeleeAtaack()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(MeleeMotion());

    }

    IEnumerator MeleeMotion()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        trail.enabled = true;
        list.Clear();
        attackCollider.enabled = true;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        trail.enabled = false;
        attackCollider.enabled = false;
        ApplyDamage();
    }

    List<GameObject> list = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!list.Contains(other.gameObject))
            list.Add(other.gameObject);
    }
    private void ApplyDamage()
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].TryGetComponent<ICharBase>(out ICharBase charBase))
            {
                charBase.TakeDamage(damage);
            }
        }
    }

}
