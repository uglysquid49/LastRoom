using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomExit : MonoBehaviour
{
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(nextScene);
    }
}
