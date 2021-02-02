using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float moveSpeed;
    public float jumpSpeed;
    public float moveInput;
    

    private bool isOnGround;
    public Transform playerPos;
    public float positionRadius;
    public LayerMask ground;

    private float airTimeCount;
    private float airTime;
    public bool inAir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if(moveInput > 0)
        {
            sr.flipX = false;
        } else if (moveInput < 0)
        {
            sr.flipX = true;
        }

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            inAir = true;
            airTimeCount = airTime;
            rb.velocity = Vector2.up * jumpSpeed;
        }

        if (Input.GetKey(KeyCode.Space) && inAir == true)
        {
            if (airTimeCount > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                airTimeCount -= Time.deltaTime;
            }
            else
            {
                inAir = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            inAir = false;
        }



    }






}
