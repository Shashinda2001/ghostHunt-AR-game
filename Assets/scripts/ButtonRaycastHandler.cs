using UnityEngine;
using UnityEngine.UI;

public class ButtonRaycastHandler : MonoBehaviour
{
    public Camera arCamera; // Assign the AR Camera
    public Button raycastButton; // Assign the button in the Canvas
    public float maxRayDistance = 100f; // Maximum distance for the raycast
    public LayerMask raycastLayerMask; // Optional: Layer mask for the raycast
    public GameObject firePrefab;
    void Start()
    {
        if (raycastButton != null)
        {
            // Add listener for button click
            raycastButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Raycast Button is not assigned!");
        }
    }

    void OnButtonClick()
    {
        // Get the center screen point
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        // Create a ray from the camera through the screen center
        Ray ray = arCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, maxRayDistance, raycastLayerMask))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object has the tag "Ghost"
            if (hitObject.CompareTag("ghost"))
            {
                Debug.Log("Hit Ghost: " + hitObject.name);
                Destroy(hitObject); // Destroy the hit object
                                    // Instantiate fire at the hit object's position
                GameObject fire = Instantiate(firePrefab, hitObject.transform.position, Quaternion.identity);

                // Destroy the fire after 1 second
                Destroy(fire, 0.5f);

            }
            else
            {
                Debug.Log("Hit object is not a Ghost: " + hitObject.name);
            }

        }
        else
        {
            Debug.Log("No object detected.");
        }
    }
}
