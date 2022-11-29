using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Redcode.Pools;

public class MonsterBase : MonoBehaviour, ICharBase, IPoolObject
{

    private UnitState state = new UnitState();
    public UnitState STATE
    {
        get => state;
    }
    private Material material;
    private Animator animator;
    private NavMeshAgent agent;
    private MonsterAI monsterAI;
    private SpawnManager spawn;

    private int uid;

    [SerializeField]
    private string poolName;
    public string POOLNAME
    {
        get => poolName;
    }





    private void Awake()
    {
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        monsterAI = GetComponent<MonsterAI>();
        spawn = transform.parent.GetComponent<SpawnManager>();



    }
    public void InitMonster(int tableUID)
    {
        uid = tableUID;
        GameManager.Inst.GetMonsterData(tableUID, out TableMonster monsterData);

        state.currentHP = monsterData.maxHP;
        state.maxHP = monsterData.maxHP;
        state.defence = 2;
        state.attackRange = 4f;
        state.attackRate = 1f;
        state.attackDamage = monsterData.attackDamage;
        agent.speed = monsterData.moveSpeed;
        material.color = Color.white;
        monsterAI.InitAI();
    }

    public void OnCreatedInPool()
    {
    }

    public void OnGettingFromPool()
    {
        //InitMonster();
    }

    private void Update()
    {
        Locomotion();
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
        monsterAI.ChangeAIState(AI_State.Die);
        yield return YieldInstructionCache.WaitForSeconds(2f);
        spawn.ReturnPool(this);
    }

    [SerializeField]
    private GameObject dropItem;
    private void DropItem()
    {
        Instantiate(dropItem, transform.position, Quaternion.identity);
        //Random.Range(1001, 1011)
        //GameManager.Inst.GetMonsterData(uid, out TableMonster monsterData);
        //if (Random.Range(0, 10001) < monsterData.dropRate)
            
        //else
        //    Debug.Log("드랍 실패");
    }


    #region Anims
    public ICharBase attackTarget;
    public void AttackTarget(ICharBase target)
    {
        animator.SetTrigger("Attack 01");
        attackTarget = target;
        Invoke("ApplyDamage", 0.25f);

    }


    void Locomotion()
    {
        //Run Forward
        if (agent.velocity.sqrMagnitude > 0.1f)
            animator.SetBool("Run Forward", true);
        else
            animator.SetBool("Run Forward", false);
    }

    #endregion
    void ApplyDamage()
    {
        attackTarget.TakeDamage(STATE.attackDamage);
    }


}
