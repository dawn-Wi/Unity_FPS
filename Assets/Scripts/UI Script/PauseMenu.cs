using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _goBaseUi;
    [SerializeField] private SaveAndLoad _theSaveAndLoad;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager._isPause)
            {
                CallMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    private void CallMenu()
    {
        GameManager._isPause = true;
        _goBaseUi.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        GameManager._isPause = false;
        _goBaseUi.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        Debug.Log("save");
        _theSaveAndLoad.SaveData();
    }

    public void ClickLoad()
    {
        Debug.Log("Load");
        _theSaveAndLoad.LoadData();
    }
    
    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
