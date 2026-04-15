using UnityEngine;

[CreateAssetMenu(menuName = "Game/Memory")]
public class MemoryData : ScriptableObject
{
    public string memoryName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Header("Effects")]
    public int reputationCost;
    public bool loseTrust;
    public bool unlockEndingFlag;
}
