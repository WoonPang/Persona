using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 7f;
    private float moveX;
    public int lastDirection;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float checkRadius = 0.35f;
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 충돌 시 회전 방지
    }

    void Update()
    {
       
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    void LateUpdate()
    {
        SetDirec();

        HandleJumpAnimations();

        if (moveX != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    void SetDirec()
    {
        if (moveX > 0) lastDirection = 1;
        else if (moveX < 0) lastDirection = -1;

        if (lastDirection == 1) sprite.flipX = false;
        else sprite.flipX = true;
    }

    void HandleJumpAnimations()
    {
        // Animator에 isGrounded 값을 전달
        anim.SetBool("isGround", isGrounded);

        // y축 속도 값 전달
        float yVelocity = rb.linearVelocity.y;
        anim.SetFloat("yVelocity", yVelocity);
    }

}
