using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnDelay = 1.5f;
    public AudioSource audioSource;
    public AudioClip dieClip;

    public void Die()
    {
        if (audioSource != null && dieClip != null)
        {
            audioSource.PlayOneShot(dieClip);
        }

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = respawnPoint.position;
    }
}
