using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 100f;
    public int jumpCountMax = 2;
    public GameManager gameManager;

    private int jumpCount;
    private Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded = true;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.AddScore(10);
        }

        if(isDead)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //Force : FixedUpdat에서 움직일때, Impulse : 한번에 힘 줄때
            jumpCount++;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead && collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic; //물리 적용 안받게 설정
        rb.linearVelocity = Vector2.zero;

        gameManager.OnPlayerDead();
    }
}
