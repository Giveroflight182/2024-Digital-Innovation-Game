using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject creepPrefab; // Assign your creep prefab in the inspector
    public GameObject[] spawnLocations; // Array for spawn locations
    public int numberOfEnemiesToSpawn = 3; // Number of enemies to spawn
    public Vector3 spawnPosition;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Choose a random spawn location
            GameObject spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
            Vector3 spawnPosition = spawnLocation.transform.position;

            // Instantiate the creep prefab at the spawn location
            GameObject enemyInstance = Instantiate(creepPrefab, spawnPosition, Quaternion.identity);

            // Configure the enemy properties
            EnemyControl enemyControl = enemyInstance.GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                // Set enemy properties, you can customize these as needed
                enemyControl.health = 100; // Example health value
                enemyControl.weapon = "Ball"; // Example weapon
                enemyControl.model = "CreepModel"; // Example model
                enemyControl.color = Color.green; // Example color

                // Call the initialization method if necessary (already done in Awake)
                //enemyControl.InitializeEnemy(); // Uncomment if you want to call this explicitly
            }
        }
    }
}
