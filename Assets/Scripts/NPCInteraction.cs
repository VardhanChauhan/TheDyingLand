using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public int stopNumber = 1;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
        audioSource.volume = 1f;
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(stopNumber, this);
    }

    public void Speak(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.ignoreListenerPause = true;
        audioSource.Play();

        Debug.Log("🔊 NPC speaking: " + clip.name);
    }

    public void StopSpeaking()
    {
        audioSource.Stop();
    }
}
