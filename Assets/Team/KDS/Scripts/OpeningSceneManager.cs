using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneManager : MonoBehaviour
{
    private const string FirstTimeKey = "FirstTime";

    void Start()
    {
        Debug.Log("FirstTime 값: " + PlayerPrefs.GetInt(FirstTimeKey, 1));
        if (IsFirstTime())
        {
            Debug.Log("처음시작입니다");
            ShowOpeningScene();
            SetFirstTimeFlag(false); 
        }
        else
        {
            Debug.Log("처음시작아니다");
            Destroy(this);
        }
    }

    public bool IsFirstTime()
    {
        Debug.Log("IsFirstTime() 호출 - FirstTime 값: " + PlayerPrefs.GetInt(FirstTimeKey, 1));
        return PlayerPrefs.GetInt(FirstTimeKey, 1) == 1; 
    }

    private void SetFirstTimeFlag(bool isFirstTime)
    {
        int value = isFirstTime ? 1 : 0;
        PlayerPrefs.SetInt(FirstTimeKey, value);
        PlayerPrefs.Save();
        Debug.Log("FirstTime 설정: " + value);
    }

    private void ShowOpeningScene()
    {
        SceneManager.LoadScene("OpeningScene");
    }
}
