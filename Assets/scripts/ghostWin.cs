using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ghostWin : MonoBehaviour
{
    public float detectionRange = 5f;  // The range in which to detect objects
    public string targetTag = "Rock";  // The tag of the object you're detecting

    void Update()
    {
        // Call the method to check for objects within range
        DetectObjectsInRange();
    }

    void DetectObjectsInRange()
    {
        // Perform a sphere cast around the position of the current object
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        // Iterate over all objects in the range
        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the detected object has the desired tag
            if (hitCollider.CompareTag(targetTag))
            {
                // If it does, print the name of the object
                Debug.Log("Detected object: " + hitCollider.gameObject.name);

                // Optionally, you can trigger other actions here
                // Example: Destroy the object
                // Destroy(hitCollider.gameObject);
                SceneManager.LoadScene(0);
            }
        }
    }

    // Optional: Visualize the detection range in the editor for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}