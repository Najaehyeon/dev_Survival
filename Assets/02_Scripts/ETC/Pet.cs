using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public bool isUse;
    public float PetCoolTime;
    private float petCooltime;
    public int petStress;
    
    [SerializeField] private Image timerImage;

    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        isUse = true;
        petCooltime = PetCoolTime;
    }

    public void DownStress()
    {
        isUse = false;
        SoundManager.Instance.PlayClip(audioClip);
        GameManager.Instance.ChangeStress(-petStress);
        StartCoroutine(PetTimer());
    }

    IEnumerator PetTimer()
    {
        while (!isUse)
        {
            petCooltime -= Time.deltaTime;
            timerImage.fillAmount = 1 - (petCooltime / PetCoolTime);
            if (petCooltime < 0)
            {
                isUse = true;
                petCooltime= PetCoolTime;
                timerImage.fillAmount = 0;
            }
            yield return null;
        }
    }
}
