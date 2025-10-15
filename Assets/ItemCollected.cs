using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ItemCollected : MonoBehaviour, IInteractable
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    [TextArea]
    [SerializeField] string dialogueMessage = "";
    public float displayTime = 2f;

    [Header("Audio")]
    public AudioClip pickupSFX;       // Audio clip to play on pickup
    public float sfxVolume = 1f;      // Volume of the audio

    private bool isPlayerNear = false;
    private bool hasInteracted = false;
    private AudioSource audioSource;

    private void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Add an AudioSource if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            Interact();
        }
    }

    public bool CanInteract() => isPlayerNear && !hasInteracted;

    public void Interact()
    {
        hasInteracted = true;

        // Play the pickup sound
        if (pickupSFX != null && audioSource != null)
            audioSource.PlayOneShot(pickupSFX, sfxVolume);

        // Show UI panel
        if (dialoguePanel != null && dialogueText != null)
        {
            dialogueText.text = dialogueMessage;
            dialoguePanel.SetActive(true);
            StartCoroutine(HideUIAndDestroy());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator HideUIAndDestroy()
    {
        yield return new WaitForSeconds(displayTime);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        Destroy(gameObject);
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

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }
    }
}
