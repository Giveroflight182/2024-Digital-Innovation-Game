using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management

public class Death : MonoBehaviour
{
    public int health = 100;

void Update() // or your game loop method
{
    while (health > 0)
    {
        

        
        health -= 10; // Reduce health for demonstration purposes

        // Optional: Add a delay to prevent a tight loop
        System.Threading.Thread.Sleep(100); // Pause for 100 milliseconds
    }

    Debug.Log("Dead");
}


    

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
