using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Intro,
    Title,
    Loading,
    BaseTown,
    Battle,
    Boss,

}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Inst
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private SceneName nextScene;
    public SceneName NextScene
    {
        get { return nextScene; }
    }

    public void AsyncLoadNextScene(SceneName scene)
    {
        nextScene = scene;
        SceneManager.LoadScene(SceneName.Loading.ToString());
    }
}
