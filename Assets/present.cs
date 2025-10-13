using System;
using UnityEngine;

public class InteractiveItem : MonoBehaviour, IInteractable
{
    public bool IsOpened {  get; private set; }

    public string PresentID { get; private set; }
    public GameObject ItemPrefab;
    public Sprite openedSprite;
    private bool isPlayerNear;

    //start is called before the first frame update

    void Start()
    {
        PresentID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        return !IsOpened;
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
    }


    private void Opened()
    {
       SetOpened(true);

        //DropItem
        if (ItemPrefab)
        {
            GameObject droppedIten = Instantiate(ItemPrefab, transform.position + Vector3.down, Quaternion.identity);
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
        }
    }

}
