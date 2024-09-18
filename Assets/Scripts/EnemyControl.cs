using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    public float jumpForce = 10f; // The force applied when jumping
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer to consider as ground
    private bool isGrounded;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Rigidbody rb;
    private bool jumpRequested;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // Adjust Rigidbody constraints and center of mass
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Adjust as needed
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        // Check if the enemy is grounded
        isGrounded = CheckIfGrounded();

        // Request jump if the velocity is close to zero and the enemy is grounded
        if (Mathf.Approximately(rb.velocity.magnitude, 0f) && isGrounded && !jumpRequested)
        {
            jumpRequested = true; // Prevent multiple jumps
            Jump();
        }
    }

    private bool CheckIfGrounded()
    {
        // Use a raycast to check if the AI is grounded
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void Jump()
    {
        if (agent.enabled)
        {
            agent.enabled = false; // Temporarily disable the NavMeshAgent
        }

        // Ensure no rotation is applied and reset vertical velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.angularVelocity = Vector3.zero; // Reset any angular velocity

        // Re-enable the NavMeshAgent after a short delay
        Invoke(nameof(ReenableNavMeshAgent), 1f);
    }

    private void ReenableNavMeshAgent()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        jumpRequested = false; // Allow jumping again
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure the enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
