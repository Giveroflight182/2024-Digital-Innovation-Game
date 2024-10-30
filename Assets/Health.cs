using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
    }

    public void Damaged(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health is 0 or less
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed max
    }

    public bool IsAlive()
    {
        return currentHealth > 0; // Check if alive
    }

    private void Die()
    {
        // Handle death logic (e.g., playing an animation, disabling the object, etc.)
        Debug.Log(gameObject.name + " has died.");
        // Optionally, you can disable the GameObject or trigger a respawn
        gameObject.SetActive(false); // Disable the GameObject
    }

    public int GetCurrentHealth()
    {
        return currentHealth; // Optional: Method to get current health
    }
}
