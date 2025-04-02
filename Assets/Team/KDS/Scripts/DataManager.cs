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
        GameData gameData = new GameData
        {
            Money = GameManager.Instance.Money,
            Day = GameManager.Instance.Day,
            Stress = GameManager.Instance.Stress,
            hiredEmployeeIDs = UIManager.Instance.shopUI.employShop.hiredEmployeeIDs,
        };

        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(gameDataPath, json);
        Debug.Log("게임 데이터 저장됨: " + gameDataPath);
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
            foreach(int hireindex in gameData.hiredEmployeeIDs)
            {
                EmployeeManager.Instance.HireEmployee(hireindex);
            }
            Debug.Log("게임 데이터 로드됨");
        }
        else
        {
            Debug.Log("저장된 게임 데이터가 없습니다.");
        }
    }
    public void DeleteGameManager()
    {
        if (File.Exists(gameDataPath))
        {
            File.Delete(gameDataPath);
            Debug.Log("게임 데이터 삭제됨: " + gameDataPath);
        }
        else
        {
            Debug.Log("삭제할 게임 데이터가 존재하지 않습니다.");
        }
    }
}


