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


public class PlayerData  // 세이블 파일을 만들기 위한 데이터 
{
    public string userNickName;
    public int level;
    public int curExp;
    public int gold;
    public Inventory inventory = new Inventory();
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

    private int stageLevel = 3;
    public int STAGELEVEL
    { get => stageLevel; }


    private void Awake()
    {
        base.Awake();
        pData = new PlayerData();
        dataPath = Application.persistentDataPath + "/save";
        stageLevel = 1;

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

        for(int i = 0; i < table.ItemData.Count;i++)
        {
            itemData.Add(table.ItemData[i].uid, table.ItemData[i]);
        }

        for(int i = 0; i < table.MonsterData.Count; i++)
        {
            monsterData.Add(table.MonsterData[i].uid, table.MonsterData[i]);
        }
        #endregion

        LoadData();
    }
    #region saveData
    private string dataPath;

    public void SaveData()
    {
        string data = JsonUtility.ToJson(pData);
        //  암호화 시키는걸 꼭 해주셔야 해요 

        Debug.Log("저장" + data);
        File.WriteAllText(dataPath, data);
    }
    public bool LoadData()
    {
        if(File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            // 암호화된 데이터를 복호화 
            Debug.Log("불러오기" + data);
            pData = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log("확인" +  pData.inventory.GetItemList().Count);
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

    #region ItemLogic
    private Dictionary<int, TableItem> itemData = new Dictionary<int, TableItem>();

    public bool GetItemData(int uid, out TableItem item)
    {
        return itemData.TryGetValue(uid, out item);
    }

    public Dictionary<int, TableMonster> monsterData = new Dictionary<int, TableMonster>();

    public bool GetMonsterData(int uid, out TableMonster monster)
    {
        return monsterData.TryGetValue(uid, out monster);
    }

    #endregion
    #region updateUserData
    public void UpdateNickName(string newNickName)  //회원가입했을때 신규 데이터
    {
        pData.userNickName = newNickName;
        pData.curExp = 0;
        pData.gold = 0;
        pData.level = 1;
        SaveData();
    }



    #endregion


    #region PlayerDataRead
    public int PlayerGold
    {
        get => pData.gold;
        set => pData.gold = value;
    }

    public string PlayerName
    {
        get => pData.userNickName;
    }
    public int PlayerLevel
    {
        get => pData.level;
    }
    public int PlayerCurrentEXP
    {
        get => pData.curExp;
    }
    public Inventory Inventory
    {
        get => pData.inventory;
    }

    public bool LootingItem(InventoryItemData item)
    {
        if(!pData.inventory.IsFull())
        {
            pData.inventory.AddItem(item);
            return true;
        }
        return false;
    }
    public void DeleteItem(InventoryItemData item)
    {
        pData.inventory.DeleteItem(item);
    }

    #endregion

}
