using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RoomChange : MonoBehaviour
{
    public int sceneBuilderIndex;
    public float enterSpeed = 1f;
    public GameObject fadeAnimation;
    public Canvas canvas;
    private Animator transitionAnimator;
    private object playerBodyBody;

    private void Start()
    {
        if (sceneBuilderIndex == null)
        {
            //Canvas
            canvas = FindObjectOfType<Canvas>();
        }

        if (fadeAnimation == null)
        {
            Debug.Log("has no fadeAnimation set for transition");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Could use other .GetComponent<Player>()
        if (other.tag == "Player")
        {
            print("Switching Scene to" + sceneBuilderIndex);
            SceneManager.LoadScene(sceneBuilderIndex, LoadSceneMode.Single);
           
            transitionAnimator = Instantiate(fadeAnimation, canvas.transform).GetComponent<Animator>();
        }



    }

}