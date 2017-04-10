using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameStateInitialization : MonoBehaviour {

    public delegate void OnLoaded(int lvl);
    public OnLoaded _OnLoaded;

    public delegate void Unload();
    public Unload _Unload;

	void Start () {
        SceneManager.sceneLoaded += GameStateInitialize;
	}
	
    void GameStateInitialize (Scene scene, LoadSceneMode mode)
    {
        _OnLoaded(GetComponent<LevelLoader>().GetLevel());
    }

    public void GameStatePreUnload ()
    {
        _Unload();
    }

}
