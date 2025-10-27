using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] string nextScene;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    public float speed = 3f;

    private Rigidbody2D rb;
    private Transform currentPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;
    }

    void Update()
    {
        // Move toward the current point
        Vector2 direction = ((Vector2)currentPoint.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // When close enough, switch to next point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.2f)
        {
            if (currentPoint == pointA.transform) currentPoint = pointB.transform;
            else if (currentPoint == pointB.transform) currentPoint = pointC.transform;
            else if (currentPoint == pointC.transform) currentPoint = pointD.transform;
            else currentPoint = pointA.transform; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))

        {
            SceneManager.LoadScene(nextScene);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA.transform.position, 0.3f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.3f);
        Gizmos.DrawWireSphere(pointC.transform.position, 0.3f);
        Gizmos.DrawWireSphere(pointD.transform.position, 0.3f);
    }
}
