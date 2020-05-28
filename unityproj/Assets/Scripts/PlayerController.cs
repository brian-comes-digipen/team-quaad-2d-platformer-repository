using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private BoxCollider2D boxCol;

    private bool isDead;

    private bool jumpCooldown;
    
    private bool isWallGrabbing;

    private float fallDelayLeft = 0;

    private Rigidbody2D rb2D;

    private Collision col;


    #endregion Private Fields

    #region Public Fields

    [Header("?")]
    public static float playerHeight;

    public static float playerOffset;

    [Header("Abilities")]
    public bool CanCrouch = false;

    public bool CanJump = true;

    public bool CanJumpTwice = true;

    public bool CanMove = true;

    public bool CanPunch = false;

    public bool CanPushPull = false;

    public bool CanSprint = false;

    public bool CanWallClimb = false;

    public bool CanWallGrab = false;

    public bool CanWallJump;

    public bool CanWallSlide;
    
    bool flipX;

    [Header("Items")]
    public bool HasFlashlight = false;

    public bool HasKeyBlue = false;

    public bool HasKeyRed = false;

    public bool HasKeyYellow = false;

    [Header("Other Stuff")]
    public Vector2 respawnPos;

    public GameObject hitBoxPunch = null;

    // Y coordinate at which the player dies / respawns
    public float respawnYValue = -5f;

    [Header("Stats")]
    public int health = 6; // player has three hearts, but since there are half hearts (making 6 total halves), the player's max health is 6

    public int livesLeft = 3;
    
    public float jumpHeight = 4.5f;

    public float fallMultiplier = 1.5f;

    public int jumpsLeft = 1;

    //public float jumpVelocity;

    public float walkSpeed = 5f;

    public float climbSpeed = 2.5f;

    public float fallSpeed = 3f;

    public float fallDelay = 0.2f;

    Vector2 vel;

    SpriteRenderer sr;

    #endregion Public Fields

    #region Private Methods


    private void WallClimb()
    {
        //Wall Grab
        if (CanWallGrab && col.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            isWallGrabbing = true;
            jumpsLeft = 1;
            rb2D.gravityScale = 0;
        }
        else
        {
            isWallGrabbing = false;
            rb2D.gravityScale = 1;
        }
    }
    
    private void Movement()
    {
        vel = rb2D.velocity;

        if (isWallGrabbing)
        {
            //While latched to a wall, player cannot move left or right
            vel.x = 0;
            CanMove = false;
            CanWallClimb = true;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                vel.y = climbSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                vel.y = -climbSpeed;
            }
            else
            {
                vel.y = 0;
            }
        }
        else
        {
            CanMove = true;

            // Sets the RigidBody's velocity equal to our own velocity

            // Left and Right Controls

            if (Input.GetKey(KeyCode.RightArrow))
            {
                vel.x = walkSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                vel.x = -walkSpeed;
            }

            // stop it from creeping forever, adds delay when movement is 0/null
            if (Mathf.Abs(vel.x) <= walkSpeed / 7)
            {
                vel.x = 0;
            }
            else
            {
                vel.x = vel.x / 2;
            }
        }

        // Sets our own velocity equal to the value of the Rigidbody velocity
        rb2D.velocity = vel;
    }

    private void Jump()
    {
        if (!isWallGrabbing)
        {
            vel = rb2D.velocity;

            if (CanJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && jumpsLeft > 0)
            {
                vel = Vector2.up * jumpHeight;
                jumpsLeft--;
            }
            else if (rb2D.velocity.y == 0 && rb2D.IsTouchingLayers())
            {
                if (CanJumpTwice)
                    jumpsLeft = 2;
                else
                    jumpsLeft = 1;
            }

            if (rb2D.velocity.y < 0)
            {
                if(col.onWall)
                {
                    fallDelayLeft -= Time.deltaTime;
                    if(fallDelayLeft <= 0)
                        vel.y = -fallSpeed;
                }
                else
                {
                    vel += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
            }
            else
            {
                fallDelayLeft = fallDelay;
            }
            
            rb2D.velocity = vel;
        }
    }

    private void Crouch()
    {
        if (!isWallGrabbing)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                boxCol.size = new Vector3(boxCol.size.x, playerHeight / 2.5f);
                boxCol.offset = new Vector3(boxCol.offset.x, playerOffset - playerHeight / 4.2f);

                //ChangeState(AnimationState.Crouch);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                boxCol.size = new Vector3(boxCol.size.x, playerHeight);
                boxCol.offset = new Vector3(boxCol.offset.x, playerOffset);

                //ChangeState(AnimationState.Walk);
            }
        }
    }
    
    private void Respawn()
    {
        if (transform.position.y <= respawnYValue)
        {
            isDead = true;
        }
        if(isDead)
        {
            transform.position = respawnPos;
            isDead = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ItemPickup>() != null)
        {
            // Item pickup
        }
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            isDead = true;
        }
    }


    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        col = GetComponent<Collision>();
        playerHeight = boxCol.size.y;
        playerOffset = boxCol.offset.y;
        respawnPos = transform.position;

    }

    // Update is called once per frame
    private void Update()
    {
        WallClimb();
        Movement();
        Jump();
        Crouch();
        Respawn();

        sr = GetComponent<SpriteRenderer>();

        bool flipX = vel.x < 0;
        if (flipX != sr.flipX && vel.x != 0)
        {
            sr.flipX = flipX;
            //1.76423455f * 2 : -1.76423455f * 2;   <- Used when center is the top left corner
            float adjustX = flipX ? 0 : 0;
            sr.transform.Translate(adjustX, 0, 0);

        }
    }


    #endregion Private Methods
}
