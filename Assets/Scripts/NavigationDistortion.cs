using UnityEngine;

public class NavigationDistortion : MonoBehaviour
{
    public float distortionChance = 0.4f;
    public float maxAngleError = 90f;

    Vector3 distortedForward;

    void Update()
    {
        if (MemoryManager.Instance == null)
            return;

        bool hasNavigation = MemoryManager.Instance.HasMemory("Navigation");

        if (!hasNavigation && Random.value < distortionChance)
        {
            float angle = Random.Range(-maxAngleError, maxAngleError);
            distortedForward = Quaternion.Euler(0, angle, 0) * transform.forward;
        }
        else
        {
            distortedForward = transform.forward;
        }
    }

    public Vector3 GetForward()
    {
        return distortedForward;
    }
}
