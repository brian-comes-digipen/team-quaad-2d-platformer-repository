using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private BoxCollider2D boxCol;

    private bool isDead;

    private bool jumpCooldown;

    private Rigidbody2D rb2D;

    private Collision col;

    #endregion Private Fields

    #region Public Fields

    [Header("")]
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

    public bool CanWallGrab;

    public bool CanWallJump;

    public bool CanWallSlide;

    public float fallSpeed = 2f;

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

    public float jumpHeight = 3f;

    public int jumpsLeft = 1;

    public float walkSpeed = 3f;

    public float sprintSpeed = 5f;

    #endregion Public Fields

    #region Private Methods

    private void Crouch()
    {
        if (!CanWallGrab)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                boxCol.size = new Vector3(boxCol.size.x, playerHeight / 2);
                boxCol.offset = new Vector3(boxCol.offset.x, playerOffset - playerHeight / 4);

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

    private IEnumerator DoubleJumpCoolDown()
    {
        jumpCooldown = true;
        yield return new WaitForSeconds(Time.deltaTime * 8);
        jumpCooldown = false;
    }

    private void Jump()
    {
        if (!CanWallGrab)
        {
            Vector2 vel = rb2D.velocity;

            if (CanJump && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)) && jumpsLeft > 0 && !jumpCooldown)
            {
                vel.y = jumpHeight * 1.4f;
                jumpsLeft--;
                StartCoroutine(DoubleJumpCoolDown());
            }
            else if (rb2D.velocity.y == 0 && rb2D.IsTouchingLayers())
            {
                StopCoroutine(DoubleJumpCoolDown());
                if (CanJumpTwice)
                    jumpsLeft = 2;
                else
                    jumpsLeft = 1;
            }

            if (rb2D.velocity.y < 0 && !col.onGround)
                vel.y = -fallSpeed;

            //vel.y *= 1 + .6f * Time.deltaTime * rb2D.mass;

            rb2D.velocity = vel;
        }
    }

    private void Movement()
    {
        Vector2 vel = rb2D.velocity;

        if (CanWallGrab)
        {
            //While latched to a wall, player cannot move left or right
            vel.x = 0;
            CanMove = false;
            CanWallClimb = true;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                vel.y = walkSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                vel.y = -walkSpeed;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ItemPickup>() != null)
        {
            // Item pickup
        }
    }

    private void Respawn()
    {
        if (transform.position.y <= respawnYValue)
        {
            isDead = true;
            transform.position = respawnPos;
            isDead = false;
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
        Movement();
        Jump();
        Crouch();
        WallClimb();
        Respawn();
    }

    private void WallClimb()
    {
        //Wall Grab
        if (col.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            CanWallGrab = true;
        }

        if (!col.onWall || !Input.GetKey(KeyCode.LeftShift))
        {
            CanWallGrab = false;
        }
        if (CanWallGrab)
        {
            rb2D.gravityScale = 0;

            //rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        else rb2D.gravityScale = 1;
    }

    #endregion Private Methods
}
