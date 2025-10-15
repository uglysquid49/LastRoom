using System;
using TMPro;
using UnityEngine;

public class InteractiveItem : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string PresentID { get; private set; }

    [Header("References")]
    public Sprite openedSprite;
    public GameObject DialogueManager; // optional
    public LayerMask interactableLayer;

    [Header("UI Settings")]
    [Tooltip("Assign your existing UI panel (the dialogue box in your Canvas).")]
    public GameObject dialoguePanel;      // your existing UI panel
    [Tooltip("Assign the TMP_Text component inside your dialogue panel.")]
    public TMP_Text dialogueText;         // text field for dialogue
    [TextArea]
    public string dialogueMessage = "You found something interesting."; // text to display

    private bool isDialogueActive = false;

    private void Start()
    {
        PresentID ??= GlobalHelper.GenerateUniqueID(gameObject);

        // Make sure the UI starts hidden
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        OpenPresent();
        ShowDialogue();
    }

    private void OpenPresent()
    {
        Debug.Log("Interacted with the object!");
        SetOpened(true);
    }

    private void ShowDialogue()
    {
        if (dialoguePanel == null)
        {
            Debug.LogWarning("Assign your Dialogue Panel in the Inspector!");
            return;
        }

        dialoguePanel.SetActive(true);

        if (dialogueText != null)
            dialogueText.text = dialogueMessage;

        isDialogueActive = true;
        Debug.Log("Dialogue panel shown!");
    }

    private void Update()
    {
        // Close the dialogue by pressing E again
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        isDialogueActive = false;
        Debug.Log("Dialogue closed!");
    }

    public void SetOpened(bool opened)
    {
        IsOpened = opened;
        if (opened && openedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }

}
