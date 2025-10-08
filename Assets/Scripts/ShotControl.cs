using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotControl : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (playerControl1.rigth == true){
            rb.linearVelocity = Vector2.right * speed;
        } else {
            rb.linearVelocity = Vector2.left * speed;
        }
        
        Invoke("destroyShot", 1.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && SceneManager.GetActiveScene().name == "Level2")
        {
            Destroy(other.gameObject);
            destroyShot();
            playerControl1.enemykilled++;
        }

        else if (other.gameObject.tag == "Door")
        {
            destroyShot();
            playerControl1.doorHp--;
            if (playerControl1.doorHp <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void destroyShot(){
        Destroy(gameObject);
   }

}
