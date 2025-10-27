using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashOnTouch : MonoBehaviour
{
    [Header("Flash Settings")]
    public Image flashImage;          // Drag a UI Image (from Canvas)
    public float flashDuration = 1f;  // How long the image is visible
    public AudioClip flashSFX;        // Sound effect
    public float sfxVolume = 1f;      // Volume (0–1)

    private bool isFlashing = false;

    private void Start()
    {
        if (flashImage != null)
            flashImage.gameObject.SetActive(false); // Hide image at start
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFlashing)
        {
            StartCoroutine(FlashAndDestroy());
        }
    }

    private IEnumerator FlashAndDestroy()
    {
        isFlashing = true;

        AudioSource audioSource = null;

        // Play audio if assigned
        if (flashSFX != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = flashSFX;
            audioSource.volume = sfxVolume;
            audioSource.Play();
        }

        // Show image
        if (flashImage != null)
            flashImage.gameObject.SetActive(true);
        Debug.Log("Image Appears");

        // Wait for flash duration
        yield return new WaitForSeconds(flashDuration);

        // Hide image
        if (flashImage != null)
            flashImage.gameObject.SetActive(false);

        // Stop audio when image disappears
        if (audioSource != null)
            audioSource.Stop();

        // Destroy this object
        Destroy(gameObject);
    }
}
