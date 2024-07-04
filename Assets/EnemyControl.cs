using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
};

public class EnemyControl : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Wander;

    public Transform target;
    Rigidbody myRigidbody; // Rigidbody for 3D physics

    public float range = 2f;
    public float moveSpeed = 2f;

    public LayerMask playerLayer;
    public float raycastRange = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody>(); // Rigidbody for 3D physics
        target = player.transform; // Assign player's transform directly

        // Example: Freeze rotation on X and Z axes
        myRigidbody.freezeRotation = true; // Adjust based on your needs
    }

    void Update()
    {
        switch (currState)
        {
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Die:
                // Handle death behavior
                break;
        }

        // State transitions based on player's proximity
        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange, playerLayer))
        {
            // Check if the ray hit a player collider
            if (hit.collider.CompareTag("Player"))
            {
                
                Debug.Log("Player killed!");
            }
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    void Wander()
    {
        // Example: Move forward in local space
        myRigidbody.velocity = transform.forward * moveSpeed;
    }

    void Follow()
    {
        // Example: Move towards player in 3D space
        Vector3 moveDirection = (target.position - transform.position).normalized * moveSpeed;
        myRigidbody.velocity = moveDirection;

        // Example: Rotate to face the player smoothly
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void OnTriggerExit(Collider other)
    {
        // Example: Change direction on collision exit (adjust as needed for 3D)
        transform.Rotate(Vector3.up * 180f); // Flip direction
    }
}
