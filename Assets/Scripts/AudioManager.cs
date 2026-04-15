using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource bgm;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        bgm = GetComponent<AudioSource>();
    }

    public void MuteAllAudio()
    {
        if (bgm != null)
            bgm.mute = true;
    }

    public void UnmuteAllAudio()
    {
        if (bgm != null)
            bgm.mute = false;
    }
}
