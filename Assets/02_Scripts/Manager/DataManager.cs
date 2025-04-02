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
    public bool hasCat;
    public bool hasDog;
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
            hasCat = UIManager.Instance.shopUI.itemShop.hasCat,
            hasDog = UIManager.Instance.shopUI.itemShop.hasDog
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
            UIManager.Instance.shopUI.employShop.hiredEmployeeIDs = gameData.hiredEmployeeIDs;

            if (gameData.hasDog)
            {
                NPCManager.Instance.SpawnDog();
            }
            if (gameData.hasCat)
            {
                NPCManager.Instance.SpawnCat();
            }
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


