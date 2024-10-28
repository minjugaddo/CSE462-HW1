using UnityEngine;
using UnityEngine.InputSystem;

public class SelectObject : MonoBehaviour
{
    public Material selectedMaterial; // Material for selected state
    private Material originalMaterial; // Original material to revert
    private Renderer objectRenderer;

    private static int selectionCount = 0; // Track the number of selections

    private InputAction tapAction; // Tap action for detecting clicks/taps

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

                // Only register the selection if it hasn’t been selected already
                if (objectRenderer.material != selectedMaterial)
                {
                    // Change to the selected material
                    objectRenderer.material = selectedMaterial;

                    // Increase the selection count
                    selectionCount++;
                    Debug.Log($"Object selected! Total selections: {selectionCount}");

                    // Check if all three objects have been selected
                    if (selectionCount == 3)
                    {
                        Debug.Log("All three objects have been selected!");
                        // You can trigger a further action here if needed
                    }
                }
            }
        }
    }
}
