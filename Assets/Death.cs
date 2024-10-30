using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management

public class Death : MonoBehaviour
{
    public int health = 100;

    

    void OnCollisionEnter(Collision col)
    {
        // Check if you collide with the player
        if (col.gameObject.CompareTag("Bad")) // Use CompareTag for better performance
        {
            // Ensure you have defined a damage value
            int damage = 10; // Example damage value
            col.gameObject.GetComponent<Health>().Damaged(damage);
        }
    }
}
