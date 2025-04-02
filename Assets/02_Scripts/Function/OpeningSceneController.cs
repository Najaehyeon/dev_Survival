using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneController : MonoBehaviour
{
    private const string FirstTimeKey = "FirstTime";

    void Start()
    {
        if (IsFirstTime())
        {
            ShowOpeningScene();
            SetFirstTimeFlag(false); 
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// 처음시작인지 판별
    /// </summary>
    public bool IsFirstTime()
    {
        return PlayerPrefs.GetInt(FirstTimeKey, 1) == 1; 
    }

    /// <summary>
    /// 시작이후 값변경
    /// </summary>
    private void SetFirstTimeFlag(bool isFirstTime)
    {
        int value = isFirstTime ? 1 : 0;
        PlayerPrefs.SetInt(FirstTimeKey, value);
        PlayerPrefs.Save();
    }

    private void ShowOpeningScene()
    {
        SceneManager.LoadScene("OpeningScene");
    }
}
