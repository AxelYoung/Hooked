using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    // If something enters collider
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If tag is hookpoint
        if(collision.tag == "Hookpoint")
        {
            // Destroy hook
            Destroy(collision.gameObject);
        }
        // If tag is player
        if(collision.tag == "Player")
        {
            // Kill player
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
