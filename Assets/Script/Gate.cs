using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private SceneName sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Inst.AsyncLoadNextScene(sceneName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
