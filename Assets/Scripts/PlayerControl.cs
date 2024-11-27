using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public int contador = 0;
    public int speed = 4;
    public float jump = 6.5f;
    public int saltos = 0;
    public GameObject itemFinal;

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
            saltos = 1;
        } else if (Input.GetKeyDown(KeyCode.Space) && !grounded() && saltos <= 1) {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            saltos++;
        }

        // Para detectar si toca el suelo o no, se castea un rayo desde los pies del moñeco
        bool grounded() {
            RaycastHit2D touch = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);

            if (touch.collider == null) {
                return false;
            } else {
                saltos = 0; // Reiniciar el contador de saltos cuando se toca el suelo
                return true;
            }
        }

        // Salir del juego
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }

    }

    // Eliminar objetos al tocarlos y finalizar el juego
    void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Item") {
        Destroy(collision.gameObject);
        contador++;
        if (contador == 10) {
            itemFinal.SetActive(true);
        }
    }

    if (collision.gameObject.tag == "Finish") {
        Destroy(collision.gameObject);
        SceneManager.LoadScene("Escena2D");
    }
}
}
