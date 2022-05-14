using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    // Amount of screens prerendered
    public int prerender;
    // A list of prefabs
    public Prefabs[] prefabs;
    // Camera
    private Transform cam;

    // On start
    void Start()
    {
        // Camera is found via Main Camera tag and set to cam
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If cam is not 48 units below object
        if(cam.position.y >= transform.position.y - 24 * prerender)
        {
            // Spawn in objects for screen
            SpawnIn();
            // Move up 24 units (one screen)
            transform.position += new Vector3(0, 24);
        }
    }

    // Spawn in
    public void SpawnIn()
    {
        // For all prefabs in list
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Get amount in current screen based on min and max found in prefab
            int amountInScreen = Random.Range(prefabs[i].minAmount, prefabs[i].maxAmount);
            // For every needed gameobject spawned in for each prefab
            for (int b = 0; b < amountInScreen; b++)
            {
                // Spawn in prefab based on current i prefab
                GameObject currentPrefab = Instantiate(prefabs[i].prefab);
                // Create position within range of xrange and within range of 20 y units
                Vector2 curPos = new Vector2(Random.Range(-prefabs[i].xRange, prefabs[i].xRange), Random.Range(-9, 9) + transform.position.y);
                // For each position stored in prefabs
                for (int l = 0; l < prefabs[i].locations.Count; l++)
                {
                    if (Vector2.Distance(curPos, prefabs[i].locations[l]) <= prefabs[i].distancing)
                    {
                        curPos = new Vector2(Random.Range(-prefabs[i].xRange, prefabs[i].xRange), Random.Range(-9, 9) + transform.position.y);
                        l = 0;
                    }
                }
                // Keep locations short by prefabs amount x2
                if (prefabs[i].locations.Capacity >= prefabs[i].maxAmount * 3)
                {
                    prefabs[i].locations.RemoveAt(0);
                }
                // Add current position to list of positions
                prefabs[i].locations.Add(curPos);
                // Set position to random pos
                currentPrefab.transform.position = curPos;
                // Get a random float so that squared objects still exists within size constraints
                float sizeSquared = Random.Range(prefabs[i].minSize, prefabs[i].maxSize);
                // Set scale based on sizesquared
                currentPrefab.transform.localScale = new Vector3(sizeSquared, sizeSquared);
            }
        }
    }
}
