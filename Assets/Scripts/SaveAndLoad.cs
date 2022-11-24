using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public Vector3 _playerPos;
    public Vector3 _playerRot;

    public List<int> _invenArrayNumber = new List<int>();
    public List<string> _invenItemName = new List<string>();
    public List<int> _invenItemNumber = new List<int>();
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData _saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController _thePlayer;
    private Inventory _theInven;
    
    // Start is called before the first frame update
    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
    }

    public void SaveData()
    {
        _thePlayer = FindObjectOfType<PlayerController>();
        _theInven = FindObjectOfType<Inventory>();

        _saveData._playerPos = _thePlayer.transform.position;
        _saveData._playerRot = _thePlayer.transform.eulerAngles;
        
        Slot[] _slots = _theInven.GetSlot();
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item !=null)
            {
                _saveData._invenArrayNumber.Add(i);
                _saveData._invenItemName.Add(_slots[i]._item._itemName);
                _saveData._invenItemNumber.Add(_slots[i]._itemCount);
            }
        }

        string json = JsonUtility.ToJson(_saveData);
        
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        
        Debug.Log("저장완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY+SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);

            _saveData = JsonUtility.FromJson<SaveData>(loadJson);

            _thePlayer = FindObjectOfType<PlayerController>();
            _theInven = FindObjectOfType<Inventory>();
            
            _thePlayer.transform.position = _saveData._playerPos;
            _thePlayer.transform.eulerAngles = _saveData._playerRot;

            for (int i = 0; i < _saveData._invenItemNumber.Count; i++)
            {
                _theInven.LoadToInven(_saveData._invenArrayNumber[i], _saveData._invenItemName[i], _saveData._invenItemNumber[i]);
            }
        
            Debug.Log("로드완료");
        }
        else
        {
            Debug.Log("파일이 없습니다.");
        }
    }
}
