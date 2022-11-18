using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour, IcharBase
{ 
    private UnitState state = new UnitState();
    private MeshRenderer meshRenderer;
    private Material material;
    private Animator animator;

    private void Awake()
    {
        InitMonster();
    }

    void InitMonster()
    {
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        animator = GetComponent<Animator>();
        state.currentHP = 10;
        state.defense = 2;
    }

    #region Take
    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + "Stun! Time: " + damage);
        if (state.currentHP > 0)
        {
            state.currentHP -= state.CalcDmg(damage);
            if (state.currentHP < 1)
                StartCoroutine(OnDie());
            else
                StartCoroutine(OnHit());
        }
    }

    public void TakeStun(float time)
    {
        Debug.Log(gameObject.name + "Stun! Time: " + time);
    }
    #endregion

    IEnumerator OnHit()
    {
        animator.SetTrigger("Take Damage");
        for(int i = 0; i < 3; i++)
        {
            material.color = Color.red;
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
            material.color = Color.white;
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }

    IEnumerator OnDie()
    {
        animator.SetTrigger("Die");
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        material.color = Color.gray;
        DropItem();
        yield return YieldInstructionCache.WaitForSeconds(2f);
        Destroy(gameObject);
    }

    [SerializeField]
    private GameObject dropItem;

    private void DropItem()
    {
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }
}
