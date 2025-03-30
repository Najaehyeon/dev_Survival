using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[TrackColor(0.8f, 0.5f, 1f)]
[TrackClipType(typeof(DialoguePlayableAsset))]
[TrackBindingType(typeof(TextMeshPro))]
public class DialogueTrack : TrackAsset
{
}
