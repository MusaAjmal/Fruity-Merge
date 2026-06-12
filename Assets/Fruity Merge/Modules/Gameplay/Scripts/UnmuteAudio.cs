using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public class UnmuteAudio
{
    static UnmuteAudio()
    {
        EditorPrefs.SetBool("MuteAudio", false);
    }
}
#endif