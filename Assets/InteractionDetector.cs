using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public LayerMask interactableLayer;
    public InteractUI uiManager;

    private IInteractable currentInteractable;

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);
        currentInteractable = null;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                break;
            }
        }

        // Show or hide prompt
        uiManager.ShowPrompt(currentInteractable != null);

        // Interact when pressing E
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
            uiManager.OnInteracted(); // this destroys the prompt
            currentInteractable = null;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
