using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int maxCount;
    private int curCount;
    private PoolManager poolManager;
    [SerializeField]
    private string TableIndex;
    [SerializeField]
    private int spawnType;


    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
        curCount = 0;
        spawnType = 1;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine("TrySpawn");
            while (curCount < maxCount)
                SpawnUnit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StopCoroutine("TrySpawn");
    }


    IEnumerator TrySpawn()
    {
        while(true)
        {
            yield return YieldInstructionCache.WaitForSeconds(1f);

            while(curCount < maxCount)
                SpawnUnit();
        }
    }

    private Vector3 pos;

    private int monsterID;
    private void SpawnUnit()
    {
        curCount++;
        MonsterBase monster = poolManager.GetFromPool<MonsterBase>(0);
        pos = transform.position;
        pos.x += Random.Range(-2f, 2f);
        pos.y = 0f;
        pos.z += Random.Range(-2f, 2f);
        monster.transform.position = pos;
        monsterID = 100000 + spawnType + GameManager.Inst.STAGELEVEL * 1000;
        monster.InitMonster(monsterID);
    }

    public void ReturnPool(MonsterBase monster)
    {
        curCount--;
        poolManager.TakeToPool<MonsterBase>(monster.POOLNAME, monster);
    }
}
