using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject creepPrefab; // Assign your creep prefab in the inspector
    public GameObject[] spawnLocations; // Array for spawn locations
    public Enemy[] enemies = new Enemy[4];

    void Start()
    {
        
            // Create an enemy instance with specified properties
            Enemy creep = new Enemy(2, "Sword", "CreepModel", "Green");

            // Instantiate the creep prefab at each spawn location
            foreach (GameObject spawnLocation in spawnLocations)
            {
                enemies.add(new Enemy(creepPrefab, spawnLocation.transform.position, Quaternion.identity));
                
                // Optionally, you can add additional logic here to use 'creep' properties
                // e.g., setting properties on the enemyInstance or its components
            }
            for (int i = 0; i < enemies.length; i++) {
                enemies[i] = new Enemy();
            }
        
    }
}

// Example Enemy class, adjust as needed
public class Enemy
{
    public int health;
    public string weapon;
    public string model;
    public string color;

    public Enemy(int health, string weapon, string model, string color)
    {
        this.health = health;
        this.weapon = weapon;
        this.model = model;
        this.color = color;
    }
}
