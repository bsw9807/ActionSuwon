using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private GameObject logoObj;

    private void Awake()
    {
        LeanTween.moveLocalY(logoObj, 0f, 3f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocalX(logoObj, 0f, 3f).setEase(LeanTweenType.easeInSine);
        LeanTween.rotate(logoObj, Vector3.zero, 3f);
        Invoke("AutoNextScene", 3.5f);
    }

    private void AutoNextScene()
    {
        GameManager.Inst.AsyncLoadNextScene(SceneName.BaseTown);
    }
}
