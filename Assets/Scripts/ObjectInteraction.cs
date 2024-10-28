using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInteraction : MonoBehaviour
{
    public Material selectedMaterial; // Material for selected state
    private Material originalMaterial; // Original material to revert
    private Renderer objectRenderer;
    private InputAction tapAction; // Tap action for detecting clicks/taps
    private int currentStep = 0; // Track the current interaction step
    private bool isRotating = false; // Flag to avoid multiple rotations

    private void Awake()
    {
        // Set up the Tap action in the Input System
        tapAction = new InputAction(type: InputActionType.Button, binding: "<Pointer>/press");
        tapAction.performed += OnTapPerformed;
        tapAction.Enable();
    }

    private void OnDestroy()
    {
        // Unsubscribe and disable the tap action
        tapAction.performed -= OnTapPerformed;
        tapAction.Disable();
    }

    private void Start()
    {
        // Get the object's renderer and store the original material
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    {
        // Get the position of the pointer (mouse/tap)
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if this object was clicked/tapped
            if (hit.transform == transform)
            {
                Debug.Log("Raycast hit this object");

                // Perform actions based on the current step
                if (currentStep == 0)
                {
                    // Step 1: Change color
                    objectRenderer.material.color = Random.ColorHSV();
                    Debug.Log("Step 1 completed: Color changed");
                    currentStep++;
                }
                else if (currentStep == 1)
                {
                    // Step 2: Scale the object up
                    transform.localScale *= 1.5f;
                    Debug.Log("Step 2 completed: Object scaled up");
                    currentStep++;
                }
                else if (currentStep == 2 && !isRotating)
                {
                    // Step 3: Rotate the object
                    isRotating = true;
                    StartCoroutine(RotateObject());
                    Debug.Log("Step 3 completed: Object is rotating");
                    currentStep++;
                }
            }
        }
    }

    // Coroutine to smoothly rotate the object
    private System.Collections.IEnumerator RotateObject()
    {
        float rotationDuration = 2.0f; // Rotate over 2 seconds
        float rotationSpeed = 360f / rotationDuration; // 360 degrees over the duration
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime); // Rotate around X-axis
            elapsed += Time.deltaTime;
            yield return null;
        }

        isRotating = false; // Reset flag after rotation completes
    }

}
