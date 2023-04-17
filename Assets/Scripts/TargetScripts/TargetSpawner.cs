using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public int spawnedTargets = 0;
    [SerializeField] public int maxTargets = 5;

    public GameObject[] objectArray;
    public GameObject[] rareObjects;
    public GameObject[] superRareObjects;

    public Transform[] positionArray;

    int rng;

    private GameObject randomObject;

    private List<Transform> usedPositions = new List<Transform>();

    void SpawnRandomObject()
    {
        pickObject();

        // Get a random position from the positionArray that hasn't been used before
        Transform randomPosition = GetRandomUnusedPosition();

        // Get the position of the pivot point of the object
        Vector3 pivotPosition = randomObject.transform.position;

        // Instantiate the object at the pivot position
        GameObject spawnedObject = Instantiate(randomObject, pivotPosition, Quaternion.identity);

        // Rotate the object along the Y axis
        spawnedObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // Calculate the offset between the pivot point and the actual position of the object
        Vector3 offset = spawnedObject.transform.position - pivotPosition;

        // Add the random position to the offset to correctly position the object
        spawnedObject.transform.position = randomPosition.position + offset;

        // Add the position to the usedPositions list
        usedPositions.Add(randomPosition);

        spawnedTargets++;
    }

    void pickObject()
    {
        rng = Random.Range(0, 100);

        if (rng < 60)
        {
            // 60% chance to pick an object from the objectArray
            randomObject = objectArray[Random.Range(0, objectArray.Length)];
        }
        else if (rng >= 60 && rng < 95)
        {
            // 35% chance to spawn an object from the rareObjects array.
            randomObject = rareObjects[Random.Range(0, rareObjects.Length)];
        }
        else
        {
            // 5% chance to spawn an object from the superRareObjects array.
            randomObject = superRareObjects[Random.Range(0, superRareObjects.Length)];
        }
    }


    Transform GetRandomUnusedPosition()
    {
        // Create a list of positions that haven't been used yet
        List<Transform> unusedPositions = new List<Transform>();
        foreach (Transform position in positionArray)
        {
            if (!usedPositions.Contains(position))
            {
                unusedPositions.Add(position);
            }
        }

        // If all positions have been used, reset the usedPositions list
        if (unusedPositions.Count == 0)
        {
            usedPositions.Clear();
            foreach (Transform position in positionArray)
            {
                unusedPositions.Add(position);
            }
        }

        // Get a random position from the list of unused positions
        return unusedPositions[Random.Range(0, unusedPositions.Count)];
    }

    public void RemovePosition(Vector3 position)
    {
        foreach (Transform transform in positionArray)
        {
            if (transform.position == position)
            {
                usedPositions.Remove(transform);
                break;
            }
        }
    }

    private void Update()
    {
        while (spawnedTargets < maxTargets)
        {
            // Spawn a new target
            SpawnRandomObject();
        }
    }
}
