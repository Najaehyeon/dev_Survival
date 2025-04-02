using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public int Day;
    public int Stress;
    public List<int> hiredEmployeeIDs;
    public List<GameObject> hiredEmployees;
}

public class DataManager : Singleton<DataManager>
{

    private string gameDataPath;


    void Awake()
    {
        gameDataPath = Path.Combine(Application.persistentDataPath, "GameManager.json");
    }

    public void SaveGameManager()
    {
        Debug.Log(gameDataPath);
        GameData gameData = new GameData
        {
            Money = GameManager.Instance.Money,
            Day = GameManager.Instance.Day,
            Stress = GameManager.Instance.Stress,
            hiredEmployeeIDs = UIManager.Instance.shopUI.employShop.hiredEmployeeIDs,
            hiredEmployees = NPCManager.Instance.hiredEmployees
        };

        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(gameDataPath, json);
    }

    public void LoadGameManager()
    {
        Debug.Log(gameDataPath);
        if (File.Exists(gameDataPath))
        {
            string json = File.ReadAllText(gameDataPath);

            GameData gameData = JsonUtility.FromJson<GameData>(json);

            GameManager.Instance.Init(gameData.Money, gameData.Day, gameData.Stress);
            NPCManager.Instance.hiredEmployees = gameData.hiredEmployees;
            UIManager.Instance.shopUI.employShop.hiredEmployeeIDs = gameData.hiredEmployeeIDs;
        }
        else
        {
        }
    }
    public void DeleteGameManager()
    {
        if (File.Exists(gameDataPath))
        {
            File.Delete(gameDataPath);
        }
        else
        {
        }
    }
}


