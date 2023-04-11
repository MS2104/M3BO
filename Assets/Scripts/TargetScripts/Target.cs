using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Scoring scoringScript;
    public float health = 1f;

    [SerializeField] int scoreValue;

    public AudioSource audioFile;

    void Start()
    {
        GameObject gameObject = GameObject.Find("Player");
        scoringScript = gameObject.GetComponent<Scoring>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Target killed!");

        audioFile.Play();

        // Disable the renderer of the default child object
        Renderer renderer = transform.GetChild(0).GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Delay the destruction of the parent object until the audio clip has finished playing
        StartCoroutine(DestroyAfterAudioClip(audioFile.clip.length));
    }

    IEnumerator DestroyAfterAudioClip(float delay)
    {
        yield return new WaitForSeconds(delay);

        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner != null)
        {
            spawner.RemovePosition(transform.position);
            spawner.spawnedTargets--;
        }

        scoringScript.score += scoreValue;
        Destroy(gameObject);
    }
}