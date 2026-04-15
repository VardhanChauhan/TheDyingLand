using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [TextArea(3, 6)]
    public string text;

    public AudioClip voiceClip;

    [Header("Trade Effects")]
    public int reputationLoss;
    public string memoryLost;
}
