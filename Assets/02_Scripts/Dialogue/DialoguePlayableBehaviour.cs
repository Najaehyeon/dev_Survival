using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class DialoguePlayableBehaviour : PlayableBehaviour
{
    public string dialogueText;

    private bool isInitialized = false;
    private float timer = 0f;
    private int charIndex = 0;
    private float typingSpeed = 0.05f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshPro textMesh = playerData as TextMeshPro;
        if (textMesh == null) return;

        if (!isInitialized)
        {
            timer = 0f;
            charIndex = 0;
            textMesh.text = "";
            isInitialized = true;
        }

        // 타이핑
        timer += (float)info.deltaTime;
        while (timer >= typingSpeed && charIndex < dialogueText.Length)
        {
            textMesh.text += dialogueText[charIndex];
            charIndex++;
            timer -= typingSpeed;
        }

        if (playable.GetTime() >= playable.GetDuration())
        {
            textMesh.text = dialogueText;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        isInitialized = false;
    }
}
