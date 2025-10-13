using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    public GameObject promptUI;

    private void Start()
    {
        promptUI.SetActive(false);
    }

    public void ShowPrompt(bool show)
    {
        promptUI.SetActive(show);
    }
}
