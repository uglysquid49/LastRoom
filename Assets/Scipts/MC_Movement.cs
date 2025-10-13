using UnityEngine;

public class MC_Movement : MonoBehaviour
{
    public float speed;
    float horizontal;
    float vertical;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical);

        Vector3 moveDirection = new Vector2(horizontal, vertical);
        transform.position += moveDirection * Time.deltaTime * speed;
    }
}
