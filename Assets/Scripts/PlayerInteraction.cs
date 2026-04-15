using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NavigationDistortion nd = GetComponent<NavigationDistortion>();
            Vector3 dir = nd != null ? nd.GetForward() : transform.forward;
            Ray ray = new Ray(transform.position, dir);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                NPCInteraction npc = hit.collider.GetComponentInParent<NPCInteraction>();
                if (npc != null)
                {
                    npc.Interact();
                }
            }
        }
    }
}
