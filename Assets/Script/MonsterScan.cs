using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScan : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.parent.GetComponent<MonsterAI>().SetTarget(other.gameObject);
        }
    }
}
