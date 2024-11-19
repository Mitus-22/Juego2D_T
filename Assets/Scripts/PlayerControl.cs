using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 4;
    public int jump = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
        // Moverse en horizontal
        float inputX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);
        
        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && grounded()) {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

        }

        // Para detectar si toca el suelo o no, se caseta un rayo desde los pies del moñeco
        bool grounded() {
            RaycastHit2D touch = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);

            if (touch.collider == null) {
                return false;
            } else {
                return true;
            }

        }

    }
}