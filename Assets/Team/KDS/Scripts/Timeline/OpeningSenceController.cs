
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class OpeningSenceController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string mainSceneName = "MainScene";
    public float holdTime = 1f;

    private float clickTime = 0f;
    private bool isPush = false;

    void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    void Update()
    {
        // 마우스 클릭 감지
        if (Input.GetKey(KeyCode.E))  
        {
            if (!isPush)
            {
                isPush = true;
                clickTime = 0f;
            }

            clickTime += Time.deltaTime;

            if (clickTime >= holdTime)
            {
                SceneManager.LoadScene(mainSceneName);
            }
        }
        else
        {
            isPush = false;
            clickTime = 0f;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
