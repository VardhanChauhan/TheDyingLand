using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    public int stopNumber;
    public NPCInteraction npc; // DRAG NPC HERE

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        DialogueManager.Instance.StartDialogue(stopNumber, npc);
    }
}
