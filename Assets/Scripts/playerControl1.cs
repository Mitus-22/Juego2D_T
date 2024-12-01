using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControl1 : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 4;
    public int jump = 5;
    public GameObject Shot;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
    [SerializeField] int lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

        if (inputX > 0){ //Derecha
           sprite.flipX = false; 
        } else if (inputX < 0){  //Izquierda
           sprite.flipX = true;
        }

        //Animaciones
        if (Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow)){
              anim.SetBool("isRunning",true);
        } else {
              anim.SetBool("isRunning",false);
        }
 
        if (grounded()==false){
            anim.SetBool("isJumping",true);
        } else {
            anim.SetBool("isJumping",false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded()){
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        //Disparo
        if (Input.GetMouseButtonDown(0)){
            Instantiate(Shot, transform.position + new Vector3(0, 1.5f, 1.7f), Quaternion.identity);
        }

    }

    bool grounded(){
        RaycastHit2D touch = Physics2D.Raycast(transform.position,
                                              Vector2.down,
                                              0.2f); 
        if (touch.collider ==  null){
            return false; 
        } else {
            return true;
        }                                      
    }

   void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PowerUp"){
            Destroy(other.gameObject);
            sprite.color = Color.yellow;
            GameManager.invulnerable = true;
            Invoke("becomeVulnerable",3);

        }
   }

   void becomeVulnerable(){
        sprite.color = Color.white;
        GameManager.invulnerable = false;
   }

   public void damage(){
        lives--;
        sprite.color = Color.red;
        GameManager.invulnerable = true;
        Invoke("becomeVulnerable",1);
        if (lives < 0){
            Destroy(gameObject);
            SceneManager.LoadScene("Level1");
        }
   }

    

}
