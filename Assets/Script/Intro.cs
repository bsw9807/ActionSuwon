using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private GameObject logoObj;

    private void Awake()
    {
        LeanTween.moveLocalY(logoObj, 0f, 3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalX(logoObj, 0f, 3f).setEase(LeanTweenType.easeOutBack);
    }
}
