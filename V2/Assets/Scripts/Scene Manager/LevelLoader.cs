using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour {

    //Event Manager should be handling this but music controls are here for the time being.

    private string[] funcToLevel = new string[5];

    [SerializeField] private int _level;
    private int level
    {
        get
        {
            return _level;
        }
        set
        {
            if (value != _level)
            {
                _level = value;
                //Loss of focus (no control)
                Invoke(funcToLevel[value], 0.5f);              
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }

    void Awake()
    {
        funcToLevel[0] = "DiedLevel";
        funcToLevel[1] = "LoadLevelOne";
    }

    public void LoadLevel (int lvl)
    {
        if (CheckReady(lvl))
        {
            level = lvl;
        }
    }

    private bool CheckReady (int lvl)
    {
        return true;
    }

    private void DiedLevel()
    {
        Debug.Log("Loading Empty");
        SceneManager.LoadScene("Empty");
    }

    private void LoadLevelOne()
    {
        if (GetComponent<GameStateInitialization>()._Unload != null)
        {
            GetComponent<GameStateInitialization>().GameStatePreUnload();
        }
        SceneManager.LoadScene("Game");
    }

    private void LoadLevelTwo()
    {
        if (GetComponent<GameStateInitialization>()._Unload != null)
        {
            GetComponent<GameStateInitialization>().GameStatePreUnload();
        }
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if (level == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LoadLevel(level + 1);
            }
        }
    }
}
