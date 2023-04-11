using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2 : MonoBehaviour
{
    public float health = 1f;
    public string spawnerTag = "Spawner";
    // private SpawnTarget spawnTarget;
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
        Destroy(gameObject);
    }
}
