using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneName
{
    IntroScene,
    LoadingScene,
    TitleScene,
    BaseTown,
    BattleScene,
    BossScene,
}

public class GameManager : Singleton<GameManager>
{
    #region LoadingLogic
    private SceneName nextScene;
    public SceneName NextScene
    {
        get { return nextScene; }
    }

    public void AsyncLoadNextScene(SceneName scene)
    {
        nextScene = scene;
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }
    #endregion



    private void Awake()
    {
        base.Awake();


        for(int i = 0; i < table.TipMess.Count; i++)
        {
            if (table.TipMess[i].sceneName == SceneName.BaseTown.ToString())
                baseTownTip.Add(table.TipMess[i]);
            else if (table.TipMess[i].sceneName == SceneName.BattleScene.ToString())
                battleTip.Add(table.TipMess[i]);
            else if (table.TipMess[i].sceneName == SceneName.BossScene.ToString())
                bossTip.Add(table.TipMess[i]);
        }
    }


    #region TableLogic

    [SerializeField]
    private ActionGame table;

    private List<TableTip> battleTip = new List<TableTip>();
    private List<TableTip> bossTip = new List<TableTip>();
    private List<TableTip> baseTownTip = new List<TableTip>();


    private int rand;
    public string GetTipMessage(SceneName scene)
    {
        string result = "";

        switch (scene)
        {
            case SceneName.BaseTown:
                rand = Random.Range(0, baseTownTip.Count);
                result = baseTownTip[rand].tipText;
                break;
            case SceneName.BattleScene:
                rand = Random.Range(0, battleTip.Count);
                result = battleTip[rand].tipText;
                break;
            case SceneName.BossScene:
                rand = Random.Range(0, bossTip.Count);
                result = bossTip[rand].tipText;
                break;
        }

        return result;
    }

    #endregion


}
