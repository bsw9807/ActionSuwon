using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour, ICharBase
{

    private UnitState state = new UnitState();
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
        state.defence = 2;
    }



    #region Take
    public void TakeDamage(int damage)
    {
        if (state.currentHP > 0)
        {
            state.currentHP -= state.CalculateDamage(damage);
            if (state.currentHP < 1)
                StartCoroutine(OnDie());
            else
                StartCoroutine(OnHit());
        }
    }

    public void TakeStun(float time)
    {
        Debug.Log(gameObject.name + " 스턴을 당했습니다 시간은 " + time);
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
