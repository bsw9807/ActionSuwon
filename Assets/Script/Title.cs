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
            text.text = GameManager.Inst.PlayerInfo.userNickName + "님 환영합니다 \n 터치시 시작";
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
        InitTitleScene();
    }
    //유저데이터가 없을때 새로 생성하는 팝업에서 데이터 만들어 낼때 
    public void SaveBtn()
    {
        if (havePlayerInfo)
            GameManager.Inst.AsyncLoadNextScene(SceneName.BaseTown);
        else
        {
            LeanTween.scale(nickNamePopup, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic);
            text.enabled = false;
        }
    }

    private string newNickName = "";
    public void InputField(string input)
    {
        newNickName = input;
    }

    public void CreateUserInfo()
    {
        if (newNickName.Length >= 2)
        {
            LeanTween.scale(nickNamePopup, Vector3.zero, 0.7f).setEase(LeanTweenType.easeOutElastic);
            text.enabled = true;
            GameManager.Inst.UpdateNickName(newNickName);
            InitTitleScene();
        }
        else
            WarningText();
    }


    #region WarningText
    [SerializeField]
    private TextMeshProUGUI warningText;

    private void WarningText()
    {
        Color fromColor = Color.red;
        Color toColor = Color.red;
        fromColor.a = 0f;
        toColor.a = 1f;

        LeanTween.value(warningText.gameObject, updataValue, fromColor, toColor, 1f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(warningText.gameObject, updataValue, toColor, fromColor, 1f).setDelay(1f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void updataValue(Color val)
    {
        warningText.color = val;
    }

    #endregion

}
