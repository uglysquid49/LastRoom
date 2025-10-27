using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.scissorsCollected >= 1)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
