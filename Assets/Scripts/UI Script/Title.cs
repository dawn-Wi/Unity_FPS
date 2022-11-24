using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameMain";

    public static Title _instance;

    private SaveAndLoad _theSaveAndLoad;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClickStart()
    {
        Debug.Log("Loading");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("Load");
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        _theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        _theSaveAndLoad.LoadData();
        gameObject.SetActive(false);
        
    }

    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}