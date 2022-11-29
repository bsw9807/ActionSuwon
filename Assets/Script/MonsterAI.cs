using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AI_State
{
    Roaming, // 배회하는 상태
    Chase,   // 추적하는 상태
    ReturnHome, // 집으로 되돌아오는 상태
    Attack, // 공격하는 상태 
    Die,    // 사망 상태

}
public class MonsterAI : MonoBehaviour
{
    private AI_State state;
    private NavMeshAgent agent;
    private MonsterBase monsterBase;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterBase = GetComponent<MonsterBase>();
    }

    private Vector3 homePos;
    public void InitAI()
    {
        state = AI_State.Roaming;
        homePos = transform.position;
        ChangeAIState(AI_State.Roaming);
    }
    public void ChangeAIState(AI_State newState)
    {
        Debug.Log(gameObject.name+ "  "  + state + "에서  " + newState);
        StopCoroutine(state.ToString());
        state = newState;
        StartCoroutine(state.ToString());
    }

    void SetMoveTarget(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
    }

    public void SetTarget(GameObject newTarget)
    {
        if(state == AI_State.Roaming)
        {
            attackTarget = newTarget;
            ChangeAIState(AI_State.Chase);
        }
    }


    private Vector3 movePos;
    IEnumerator Roaming()
    {
        yield return null;

        while(true)
        {
            movePos.x = Random.Range(-3f, 3f);
            movePos.y = 0f;
            movePos.z = Random.Range(-3, 3f);
            SetMoveTarget(movePos + homePos);
            yield return YieldInstructionCache.WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    GameObject attackTarget;
    IEnumerator Chase()
    {
        yield return null;
        while(attackTarget != null)
        {
            if(GetDistanceToTarget() < monsterBase.STATE.attackRange)
            {
                ChangeAIState(AI_State.Attack);
            }
            else
            {
                SetMoveTarget(attackTarget.transform.position);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        ChangeAIState(AI_State.ReturnHome);
    }

    private float GetDistanceToTarget()
    {
        if(attackTarget != null)
        {
            return (attackTarget.transform.position - transform.position).sqrMagnitude; 
        }
        return -1f;
    }

    IEnumerator Attack()
    {
        yield return null;
        while(attackTarget != null)
        {
            if (GetDistanceToTarget() > monsterBase.STATE.attackRange)
                ChangeAIState(AI_State.Chase);
            monsterBase.AttackTarget(attackTarget.GetComponent<ICharBase>());
            yield return YieldInstructionCache.WaitForSeconds(monsterBase.STATE.attackRate);
        }
        ChangeAIState(AI_State.ReturnHome);
    }

    IEnumerator ReturnHome()
    {
        yield return null;
        SetMoveTarget(homePos);
        while(true)
        {
            yield return YieldInstructionCache.WaitForSeconds(1f);
            if (agent.remainingDistance < 1f)
                ChangeAIState(AI_State.Roaming);
        }
    }



    IEnumerator Die()
    {
        yield return null;
    }

}
