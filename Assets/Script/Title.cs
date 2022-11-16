using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Title : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject nickNamePopup;
    private bool havePlayerInfo;

    private void Awake()
    {
        InitTitleScene();
    }

    private void InitTitleScene()
    {
        if (GameManager.Inst.CheckData())
        {
            text.text = GameManager.Inst.PlayerInfo.userNickName + "님 환영합니다. \n 터치시 시작";
            havePlayerInfo = true;
        }
        else
        {
            text.text = "계속 하려면 터치 하세요";
            havePlayerInfo = false;
        }
    }

    public void DeleteBtn()
    {
        GameManager.Inst.DeleteData();
    }

    public void SaveBtn()
    {
        if (havePlayerInfo)
            GameManager.Inst.AsyncLoadNextScene(SceneName.BaseTown);
        else
        {
            LeanTween.scale(nickNamePopup, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic);
            GameManager.Inst.SaveData();
        }
    }
}
