using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private BoxCollider2D bc;

    private bool isDead;

    private bool jumpCooldown;

    private Rigidbody2D rb2D;

    #endregion Private Fields

    #region Public Fields

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

    public bool wallGrab;

    public bool wallJump;

    public bool wallSlide;

   


    [Header("Items")]
    public bool HasFlashlight = false;

    public bool HasKeyBlue = false;

    public bool HasKeyRed = false;

    public bool HasKeyYellow = false;

    [Header("Other Stuff")]
    public int health = 6; // player has three hearts, but since there are half hearts (making 6 total halves), the player's max health is 6

    public GameObject hitBoxFeet = null;

    public GameObject hitBoxPunch = null;

    public GameObject hitBoxSideL = null;

    public GameObject hitBoxSideR = null;

    public Collision collision;

    public float jumpHeight = 3f;

    public int jumpsLeft = 1;

    public int remainingLives = 3;

    public Vector2 respawnPos;

    public float respawnYValue = -5f;

    public float sprintSpeed = 5f;

    public float walkSpeed = 3f;

    public float fallSpeed = 2f;

    #endregion Public Fields

    #region Private Methods

    private void Crouch()
    {
        if(!wallGrab)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bc.size = new Vector3(bc.size.x, playerHeight / 2);
                bc.offset = new Vector3(bc.offset.x, playerOffset - playerHeight / 4);

                //ChangeState(AnimationState.Crouch);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                bc.size = new Vector3(bc.size.x, playerHeight);
                bc.offset = new Vector3(bc.offset.x, playerOffset);

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
        if (!wallGrab)
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

            if (rb2D.velocity.y < 0 &&  !collision.onGround)
                vel.y = -fallSpeed;
                //vel.y *= 1 + .6f * Time.deltaTime * rb2D.mass;

            rb2D.velocity = vel;
        }
    }

    private void Movement()
    {
        Vector2 vel = rb2D.velocity;

        if (wallGrab)
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

    private void WallClimb()
    {
        //Wall Grab
        if (collision.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            wallGrab = true;
        }

        if (!collision.onWall || !Input.GetKey(KeyCode.LeftShift))
        {
            wallGrab = false;
        }
        if (wallGrab)
        {
            rb2D.gravityScale = 0;
            //rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        else rb2D.gravityScale = 1;
        
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
        bc = GetComponent<BoxCollider2D>();
        collision = GetComponent<Collision>();
        playerHeight = bc.size.y;
        playerOffset = bc.offset.y;
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

    #endregion Private Methods
}
