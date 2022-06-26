using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D player;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontal;

    private void Awake()
    {
        //get refrences for rigidbody and animator from the game object
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //make the player flip according to the movement
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //animtor parameter
        animator.SetBool("run", horizontal != 0);
        animator.SetBool("grounded", isGrounded());

        //logic for wall jump
        if(wallJumpCooldown > 0.2f)
        {
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);

            if(onWall() && !isGrounded())
            {
                player.gravityScale = 0;
                player.velocity = Vector2.zero;
            }
            else
                player.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

    }

    private void Jump()
    {
        if (isGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpPower);
            animator.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontal == 0)
            {
                player.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                player.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 5);

            wallJumpCooldown = 0;
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool attackAllowed()
    {
        return horizontal == 0 && isGrounded() && !onWall();
    }
}