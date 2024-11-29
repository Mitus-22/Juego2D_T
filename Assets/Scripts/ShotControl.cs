using UnityEngine;

public class ShotControl : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.right * speed;
        Invoke("destroyShot", 2);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
