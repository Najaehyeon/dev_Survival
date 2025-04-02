using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialoguePlayableAsset : PlayableAsset
{
    public string dialogueText;

    /// <summary>
    /// TimeLine에서 실시간 글자 편집을 위해 만듬
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialoguePlayableBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.dialogueText = dialogueText;
        return playable;
    }
}
