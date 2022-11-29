[System.Serializable]
public class TableTip
{
    public int uid;
    public string sceneName;
    public string tipText;
}

[System.Serializable]
public class TableItem
{
    public int uid;
    public string name;
    public string iconImg;
    public int sellGold;
    public int attackDamage;
    public int attackRange;
    public int attackRate;
    public bool equip;
}

[System.Serializable]
public class TableMonster
{
    public int uid;
    public int monsterType;
    public int moveSpeed;
    public int attackDamage;
    public int maxHP;
    public int dropID;
    public int dropRate;
}

