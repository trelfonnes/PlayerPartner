using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
[CreateAssetMenu(fileName = "New Cutscene", menuName = "Cutscene Data")]

public class CutsceneData : ScriptableObject
{
    public PlayableDirector timeline;

    public GameObject[] triggers;
}
