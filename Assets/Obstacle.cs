using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // On trigger enter
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If tag is player
        if (collision.tag == "Player")
        {
            // Kill player
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
