using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    float horizontalInput;
    float moveSpeed = 4f;
    bool isFacingRight = false;
    float jumpPower = 4.5f;
    bool isJumping = false;
    bool isCollecting = false;
    private int collectedIngredients = 0;



    Rigidbody2D rb;
    
    public AudioSource pickupSound;
    public AudioSource jump;

    public Sprite Idel;
    public Sprite Running;
    public Sprite Collecting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (isCollecting)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Collecting;
        }
        else if (horizontalInput == 0f && !isJumping)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Idel;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Running;
        }


        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jump.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput > 0f || !isFacingRight && horizontalInput < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            isCollecting = true;
            Destroy(collision.gameObject);
            pickupSound.Play();
            collectedIngredients++;
            isCollecting = false;

        }
    }
}
