using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCBase : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObj;
    bool isOn = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isOn && other.CompareTag("Player"))
        {
            isOn = true;
            LeanTween.scale(popupObj, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isOn && other.CompareTag("Player"))
        {
            isOn = false;
            LeanTween.scale(popupObj, Vector3.zero, 0.7f).setEase(LeanTweenType.easeOutElastic);
        }
    }
}
