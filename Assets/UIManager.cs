using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public GameObject promptUI;

    private void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    public void ShowPrompt(bool show)
    {
        if (promptUI == null) return;

        promptUI.SetActive(show);
    }

    // Called once the player has interacted with the object
    public void OnInteracted()
    {
        if (promptUI != null)
        {
            Destroy(promptUI);
            promptUI = null;
        }
    }
}
