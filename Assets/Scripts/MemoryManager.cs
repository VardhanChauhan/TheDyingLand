using UnityEngine;
using System.Collections.Generic;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;

    public List<MemoryData> ownedMemories = new List<MemoryData>();

    public int memoryCount => ownedMemories.Count;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 🔹 compatibility
    public bool HasMemory(string memoryName)
    {
        foreach (var mem in ownedMemories)
        {
            if (mem != null && mem.memoryName == memoryName)
                return true;
        }
        return false;
    }

    // 🔹 RANDOM HIDDEN MEMORY LOSS
    public void LoseRandomMemory()
    {
        if (ownedMemories == null || ownedMemories.Count == 0)
        {
            Debug.Log("No memories to lose.");
            return;
        }

        int index = Random.Range(0, ownedMemories.Count);
        MemoryData memory = ownedMemories[index];

        ownedMemories.RemoveAt(index);

        // ✅ SAFE GameManager access
        GameManager gm = GameManager.Instance;

        if (gm != null && memory != null)
        {
            gm.reputation -= memory.reputationCost;

            if (memory.loseTrust)
                gm.hasTrust = false;
        }

        // 🔥 player ko exact memory ka naam nahi batana
        if (ScreenFeedback.Instance != null)
            ScreenFeedback.Instance.Show("A memory fades away...");

        Debug.Log("Lost Memory (hidden)");
    }
}
