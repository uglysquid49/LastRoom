using System.Collections;
using TMPro;
using UnityEngine;

public class CollectedItem : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.ScissorsCollected();
        Destroy(gameObject);
    }
}
