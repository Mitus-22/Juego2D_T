using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEditor;

public class playerControl1 : MonoBehaviour
{
    Rigidbody2D rb;
    private int jumpCount = 0;
    private int maxJumps = 2;
    public int speed = 4;
    public int jump = 6;
    public static bool rigth = true;
    public static int enemykilled = 0;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
    [SerializeField] int lives = 3;
    [SerializeField] int items = 0;
    [SerializeField] float time = 180;

    [SerializeField] GameObject Shot;
    [SerializeField] TMP_Text txtLives, txtItems, txtTime;
    [SerializeField] GameObject txtWin, txtLose;
    [SerializeField] GameObject Menu;

    bool endGame = false;

    AudioSource audioSrc;
    [SerializeField] AudioClip sndJump, sndItem, sndShot, sndDamage, sndLive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Seteo inicial
        GameManager.invulnerable = false;
        rb = GetComponent<Rigidbody2D>();
        txtLives.text = "Lives: " + lives;
        txtItems.text = "Items: " + items;
        txtTime.text = time.ToString();
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (endGame == false){

            float inputX = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

            // Rotar Sprite

            if (inputX > 0){ //Derecha
            sprite.flipX = false;
            rigth = true; 
            } else if (inputX < 0){  //Izquierda
            sprite.flipX = true;
            rigth = false;
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

            // Doble salto
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (grounded()) {
                    jumpCount = 0; // reinicia el contador al saltar desde el suelo
                    rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                    audioSrc.PlayOneShot(sndJump);
                } else if (jumpCount < maxJumps - 1) {
                    jumpCount++;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reinicia la velocidad vertical para un salto consistente
                    rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                    audioSrc.PlayOneShot(sndJump);
                }
            }
            
            //Menu de pausa
            if (Input.GetKeyDown(KeyCode.Escape)){
                if (SceneManager.GetActiveScene().name == "MainMenu") {
                }

                else if (Time.timeScale == 1) {
                    Time.timeScale = 0;
                    Menu.SetActive(true);
                }
                else {
                    Time.timeScale = 1;
                    Menu.SetActive(false);
                }
            }

            //Disparo
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.timeScale == 0) { }

                else if (Time.timeScale == 1){
                    Instantiate(Shot, new Vector3(transform.position.x, transform.position.y + 1.7f, 0), Quaternion.identity);
                    anim.SetBool("isShooting", true);
                    audioSrc.PlayOneShot(sndShot);
                }
            }

            //Abrir puerta
            if (enemykilled >= 4)
            {
                Destroy(GameObject.FindWithTag("Door"));
            }

            // Tiempo
            time = time - Time.deltaTime;
            if (time < 0){
                time = 0;
                endGame = true;
                txtLose.SetActive(true);
                Invoke("goToMenu", 2);
            }

            float min, sec;
            min = Mathf.Floor (time / 60);
            sec = Mathf.Floor (time % 60);
            txtTime.text = min.ToString("00") + ":" + sec.ToString("00");

        } else {
            rb.linearVelocity = Vector2.zero;
        }
        
    }

    // Tocando el suelo o no
    bool grounded(){
        RaycastHit2D touch = Physics2D.Raycast(transform.position,
                                              Vector2.down,
                                              0.2f); 
        if (touch.collider ==  null){
            return false; 
        } else {
            jumpCount = 0; // reinicia el contador de saltos al tocar el suelo
            return true;
        }                                      
    }

    // Items
   void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PowerUp"){
            Destroy(other.gameObject);
            sprite.color = Color.yellow;
            GameManager.invulnerable = true;
            Invoke("becomeVulnerable",3);

        }

        if (other.gameObject.tag == "Live")
        {
            Destroy(other.gameObject);
            lives++;
            audioSrc.PlayOneShot(sndLive);
            txtLives.text = "Lives: " + lives;
        }

        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
            items++;
            audioSrc.PlayOneShot(sndItem);
            txtItems.text = "Items: " + items;
            if (items == 5)
            {
                endGame = true;
                txtWin.SetActive(true);
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    Invoke("goToLevel2", 2);
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    Invoke("goToCredits", 2);
                }
            }
        }

   }

   void becomeVulnerable(){
        sprite.color = Color.white;
        GameManager.invulnerable = false;
   }

   public void damage(){

        if (!endGame){
            lives--;
        }
        audioSrc.PlayOneShot(sndDamage);
        sprite.color = Color.red;
        GameManager.invulnerable = true;
        Invoke("becomeVulnerable",1);
        if (lives < 0){
            lives = 0;
            sprite.gameObject.SetActive(false);
            endGame = true;
            txtLose.SetActive(true);
            Invoke("goToMenu", 2);

        }
        txtLives.text = "Lives: " + lives;
   }

    void goToMenu(){
        SceneManager.LoadScene("MainMenu");

    }

    void goToCredits(){
        SceneManager.LoadScene("Credits");

    }

    void goToLevel2(){
        SceneManager.LoadScene("Level2");
    }

}
