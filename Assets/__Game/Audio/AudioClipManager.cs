using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioClipManager", menuName = "Audio/AudioClipManager")]

public class AudioClipManager : ScriptableObject
{
    [System.Serializable]
    public class AudioClipEntry
    {
        public string id;
        public AudioClip clip;
    }
    public List<AudioClipEntry> audioClips = new List<AudioClipEntry>();



}
