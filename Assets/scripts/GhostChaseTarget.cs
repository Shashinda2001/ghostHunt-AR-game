using UnityEngine;

public class GhostChaseTarget : MonoBehaviour
{
    public string targetTag = "Player"; // Tag of the target to chase
    public float chaseRange = 10f; // Range within which the target will be chased
    public float moveSpeed = 5f; // Speed of the ghost

    private Transform target;
    private bool isChasing = false;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning($"No object found with tag {targetTag}");
        }
    }

    void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Chase();
        }
    }

    void Chase()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
