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
        InitAI();
        agent.SetDestination(Vector3.zero);
    }

    private Vector3 homePos;
    public void InitAI()
    {
        state = AI_State.Roaming;
        homePos = transform.position;
    }

    private void ChangeAIState(AI_State newState)
    {
        Debug.Log(gameObject.name+ "  "  + state + "에서  " + newState);
    }



}
