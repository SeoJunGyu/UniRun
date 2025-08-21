using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 100f;
    public int jumpCountMax = 2;
    public GameManager gameManager;

    public AudioClip dieAudioClip;

    private int jumpCount;
    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private bool isGrounded = true;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
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
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //Force : FixedUpdat���� �����϶�, Impulse : �ѹ��� �� �ٶ�
            jumpCount++;

            audioSource.Play();
        }
        if(Input.GetMouseButtonUp(0) && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.contacts[0].normal : ���� �浹�� ù��° �浹ü�� ����
        if (collision.collider.CompareTag("Platform") && collision.contacts[0].normal.y > 0.7f)
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
        rb.bodyType = RigidbodyType2D.Kinematic; //���� ���� �ȹް� ����
        rb.linearVelocity = Vector2.zero;

        gameManager.OnPlayerDead();

        //1. ����� �ҽ� �ٲٰ� �÷���
        //audioSource.clip = dieAudioClip;
        //audioSource.Play();

        //2. ������� �ٲٰ� �÷��� �Լ��� �ִ�.
        audioSource.PlayOneShot(dieAudioClip);
    }
}
