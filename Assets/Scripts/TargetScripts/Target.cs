using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public Scoring scoringScript;
    public ParticleSystem destroyEffect;
    public float health = 1f;

    Vector3 scoreSpawnPos = new Vector3(0f, 1f, 0f);

    [SerializeField] int scoreValue;

    public AudioSource audioFile;

    void Start()
    {
        GameObject gameObject = GameObject.Find("Player");
        destroyEffect = GetComponent<ParticleSystem>();
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

        if (destroyEffect != null)
        {
            destroyEffect.Play();
        }

        // Load the prefab from Resources folder (assuming the prefab is located in a folder named "Prefabs" in the "Resources" folder)
        GameObject scoreDisplayPrefab = Resources.Load<GameObject>("Prefabs/ScoreDisplayObj");

        // Instantiate the prefab
        GameObject scoreDisplayObj = Instantiate(scoreDisplayPrefab, transform.position + scoreSpawnPos, Quaternion.identity);

        // Set the score value text on the instantiated prefab
        TextMeshPro scoreText = scoreDisplayObj.GetComponent<TextMeshPro>();
        if (scoreText != null)
        {
            scoreText.text = $"+ {scoreValue}";
        }

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