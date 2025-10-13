using System;
using UnityEngine;

public class InteractiveItem : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }

    public string PresentID { get; private set; }
    public GameObject DialogueManager;
    public Sprite openedSprite;
    private bool isPlayerNear;
    public LayerMask interactableLayer;

    void Start()
    {
        PresentID ??= GlobalHelper.GenerateUniqueID(gameObject);
      
    }

    public bool CanInteract()
    {
        return !IsOpened;
        DialogueManager.SetActive(false);
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        OpenPresent();
    }

    void OpenPresent()
    {
        // Instead of throwing an error, do something
        Debug.Log("Interacted with the object!");
        SetOpened(true);


        if (DialogueManager)
        {
            GameObject droppedItem = Instantiate(DialogueManager, transform.position + Vector3.down, Quaternion.identity);
            
        }
    }

    public void SetOpened(bool opened)
    {
        if (IsOpened = opened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            DialogueManager.SetActive(true);
        }
    }

}