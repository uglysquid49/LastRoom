using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [Header("Dialogue Settings")]
    [TextArea(2, 5)]
    public string[] dialogueLines;            // Lines of dialogue
    public float typingSpeed = 0.03f;         // Speed of typewriter
    [Range(1, 5)]
    public int soundEveryNChars = 1;          // Play SFX every N letters
    public AudioClip typingSound;             // Typing sound effect
    [Range(0f, 1f)]
    public float typingVolume = 0.2f;         // Volume of SFX

    [Header("UI Settings")]
    public GameObject dialogueBoxPrefab;      // Prefab with TMP_Text
    public Canvas parentCanvas;               // Reference to your Canvas
    private GameObject dialogueBoxInstance;
    private TMP_Text dialogueText;

    private bool isPlayerNear = false;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private int currentLine = 0;
    private AudioSource audioSource;

    void Start()
    {
        // Setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;

        // Instantiate dialogue box and parent to Canvas
        if (dialogueBoxPrefab != null && parentCanvas != null)
        {
            dialogueBoxInstance = Instantiate(dialogueBoxPrefab);
            dialogueBoxInstance.transform.SetParent(parentCanvas.transform, false);

            dialogueText = dialogueBoxInstance.GetComponentInChildren<TMP_Text>();
            if (dialogueText == null)
                Debug.Log("TMP_Text not found in dialogue box prefab!");

            // Keep prefab position as is (fixed placement)
            dialogueBoxInstance.SetActive(false);
        }
        else
        {
            Debug.Log("Assign DialogueBoxPrefab and ParentCanvas in Inspector!");
        }
    }

    public void Interact()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
                Debug.Log("NPC is talking!");
            }
            else if (isTyping)
            {
                // Skip typewriter effect
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
                audioSource.Stop();
            }
            else
            {
                NextLine();
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        currentLine = 0;
        dialogueBoxInstance.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        int charCounter = 0;

        while (charCounter < dialogueLines[currentLine].Length)
        {
            dialogueText.text += dialogueLines[currentLine][charCounter];

            // Play typing sound every N letters
            if (typingSound != null && charCounter % soundEveryNChars == 0)
            {
                audioSource.PlayOneShot(typingSound, typingVolume);
            }

            charCounter++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        audioSource.Stop();
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBoxInstance.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (isDialogueActive)
                EndDialogue();
        }
    }
}
