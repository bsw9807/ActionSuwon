using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public enum SceneName
{
    IntroScene,
    LoadingScene,
    TitleScene,
    BaseTown,
    BattleScene,
    BossScene,
}


public class PlayerData
{
    public string userNickName;
    public int level;
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

    private PlayerData pData;
    public PlayerData PlayerInfo
    {
        get { return pData; }
    }


    private void Awake()
    {
        base.Awake();
        pData = new PlayerData();
        dataPath = Application.persistentDataPath + "/save";

        #region TableData
        table = Resources.Load<ActionGame>("ActionGame");
        for (int i = 0; i < table.TipMess.Count; i++)
        {
            if (table.TipMess[i].sceneName == SceneName.BaseTown.ToString())
                baseTownTip.Add(table.TipMess[i]);
            else if (table.TipMess[i].sceneName == SceneName.BattleScene.ToString())
                battleTip.Add(table.TipMess[i]);
            else if (table.TipMess[i].sceneName == SceneName.BossScene.ToString())
                bossTip.Add(table.TipMess[i]);
        }
        #endregion
    }
    #region saveData
    private string dataPath;

    public void SaveData()
    {
        string data = JsonUtility.ToJson(pData);
        //  암호화 시키는걸 꼭 해주셔야 해요 
        File.WriteAllText(dataPath, data);
    }
    public bool LoadData()
    {
        if(File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            // 암호화된 데이터를 복호화 
            pData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        return false;
    }

    public bool CheckData()
    {
        if(File.Exists(dataPath))
        {
            return LoadData();
        }
        return false;
    }
    public void DeleteData()
    {
        File.Delete(dataPath);
    }

    #endregion
    #region TipLogic

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

    #region updateUserData
    public void UpdateNickName(string newNickName)
    {
        pData.userNickName = newNickName;
        SaveData();
    }



    #endregion
}
