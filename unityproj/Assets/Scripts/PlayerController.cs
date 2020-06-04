using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private const int ItemLayer = 13;

    private const int EnemyLayer = 9;

    private const int HazardLayer = 10;

    private static float plrHeight;

    private static float plrOffset;

    private CapsuleCollider2D capCol2D;

    private Collision col;

    private float fallDelayLeft = 0;

    private bool isCrouching = false;

    private bool isDead = false;

    private bool isWallGrabbing = false;

    private bool isPunching = false;

    private Rigidbody2D rb2D;

    private SpriteRenderer spr;

    private Vector2 vel;

    private GameObject hitBoxPunch;

    private GameObject flameBoi;

    private Animator ani;

    private Audio aud;

    private float respawnTimer = 0;

    private Vector3 flamepos;

    #endregion Private Fields

    #region Internal Fields

    [Header("Other Stuff")]
    internal Vector3 respawnPos;

    #endregion Internal Fields

    #region Public Fields

    public static bool gamePaused;

    [Header("Abilities")]
    public bool CanCrouch = false;

    public bool CanJump = true;

    public bool CanJumpTwice = true;

    public bool CanMove = true;

    public bool CanPunch = false;

    public bool CanSprint = false;

    public bool CanWallClimb = false;

    public bool CanWallGrab = false;

    public bool CanWallJump = false;

    public bool CanWallSlide = false;

    [Header("Items")]
    public bool HasFlashlight = false;

    public bool HasKeyBlue = false;

    public bool HasKeyRed = false;

    public bool HasKeyYellow = false;

    [Header("Stats")]
    public int health = 6; // player has three hearts, but since there are half hearts (making 6 total halves), the player's max health is 6

    public float jumpHeight = 4.5f;

    public int jumpsLeft = 1;

    public int livesLeft = 3;

    public float climbSpeed = 2.5f;

    public float fallDelay = 0.2f;

    public float fallMultiplier = 1.5f;

    public float fallSpeed = 3f;

    public float fastFallSpeed = 2f;

    public int keysCollected = 0;

    public float jumpVelocity;

    public float walkSpeed = 5f;

    #endregion Public Fields

    #region Private Methods

    private void AnimateSprite()
    {
        if (vel.y == 0)
            ani.SetFloat("hSpeed", Mathf.Abs(vel.x));
        else
        {
            ani.SetFloat("vVelocity", vel.y);
            ani.SetFloat("vSpeed", Mathf.Abs(vel.y));
        }
    }

    private void Crouch()
    {
        if (CanCrouch && !isWallGrabbing)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                capCol2D.size = new Vector3(capCol2D.size.x, plrHeight / 2.5f);
                capCol2D.offset = new Vector3(capCol2D.offset.x, plrOffset - plrHeight / 4f);
                flameBoi.transform.position = new Vector3(flameBoi.transform.position.x, flameBoi.transform.position.y - 0.3f);

                ani.SetBool("isCrouching", true);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                capCol2D.size = new Vector3(capCol2D.size.x, plrHeight);
                capCol2D.offset = new Vector3(capCol2D.offset.x, plrOffset);
                flameBoi.transform.position = new Vector3(flameBoi.transform.position.x, flameBoi.transform.position.y + 0.3f);
                ani.SetBool("isCrouching", false);
            }
        }
    }

    private void Jump()
    {
        if (!isWallGrabbing)
        {
            vel = rb2D.velocity;

            if (CanJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && jumpsLeft > 0)
            {
                ani.SetBool("isJumping", true);
                aud.PlayJump();
                vel = Vector2.up * jumpHeight;
                jumpsLeft--;
            }
            else if (rb2D.velocity.y == 0 && rb2D.IsTouchingLayers())
            {
                if (CanJumpTwice)
                    jumpsLeft = 2;
                else
                    jumpsLeft = 1;

                if (col.onGround)
                    ani.SetBool("isJumping", false);
            }

            if (rb2D.velocity.y < 0)
            {
                if (col.onWall)
                {
                    fallDelayLeft -= Time.deltaTime;
                    if (fallDelayLeft <= 0)
                        vel.y = -fallSpeed;
                }
                else
                    vel += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else
                fallDelayLeft = fallDelay;

            rb2D.velocity = vel;
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
                vel.y = climbSpeed;
            else if (Input.GetKey(KeyCode.DownArrow))
                vel.y = -climbSpeed;
            else
                vel.y = 0;
        }
        else
        {
            // Sets the RigidBody's velocity equal to our own velocity

            // Left and Right Controls

            if (Input.GetKey(KeyCode.RightArrow))
            {
                vel.x = walkSpeed;
                transform.localScale = new Vector2(1, 1); // Setting the scale affects the punch hitbox too, previously the sprite was just being flipped
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                vel.x = -walkSpeed;
                transform.localScale = new Vector2(-1, 1);
            }

            // stop it from creeping forever, adds delay when movement is 0/null
            if (Mathf.Abs(vel.x) <= walkSpeed / 7)
                vel.x = 0;
            else
                vel.x = vel.x / 2;
        }

        // Sets our own velocity equal to the value of the Rigidbody velocity
        rb2D.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == ItemLayer)
        {
            aud.PlayPickup();
        }
        if (collision.gameObject.layer == EnemyLayer || collision.gameObject.tag == "Enemy")
        {
            health--;
            ani.Play("Player_Hurt");
            aud.PlayDamage();
        }
        if (collision.gameObject.layer == HazardLayer)
        {
            isDead = true;
            aud.PlayDamage();
            StartCoroutine(RespawnWait(respawnTimer)); // TODO: CHANGE THIS TO HOWEVER LONG THE DEATH ANIMATION IS???
        }
    }

    private void Punch()
    {
        if (CanPunch && Input.GetKeyDown(KeyCode.X) && !isPunching)
        {
            StartCoroutine(PunchHitboxCoroutine());
            StartCoroutine(PunchAniCoroutine());
        }
    }

    private IEnumerator PunchHitboxCoroutine()
    {
        isPunching = true;
        yield return new WaitForSeconds(.25f);
        hitBoxPunch.layer = 15;
        hitBoxPunch.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.15f); // CHANGE THIS TO HOWEVER LONG THE PUNCH ANIMATION LASTS
        hitBoxPunch.layer = 12;
        hitBoxPunch.GetComponent<SpriteRenderer>().enabled = false;
        isPunching = false;
    }

    private IEnumerator PunchAniCoroutine()
    {
        ani.SetBool("Punch", true);
        yield return new WaitForSeconds(.25f);
        ani.SetBool("Punch", false);
        yield return null;
    }

    private void WallClimb()
    {
        //Wall Grab
        if (CanWallClimb && col.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            ani.SetBool("isWallGrab", true);
            isWallGrabbing = true;
            jumpsLeft = 1;
            rb2D.gravityScale = 0;
        }
        else
        {
            ani.SetBool("isWallGrab", false);
            isWallGrabbing = false;
            rb2D.gravityScale = 1;
        }
    }

    private void Died()
    {
        isDead = health <= 0;
        if (isDead)
        {
            ani.Play("Player_Death");
            aud.PlayDied();

            //waits 1 second before respawning to play out the animation
            respawnTimer = 0.7f;
        }
    }

    private void ResetPos()
    {
        transform.position = respawnPos;
    }

    private void Respawn()
    {
        ResetPos();
        isDead = false;

        health = 6;
    }

    // Start is called before the first frame update
    private void Start()
    {
        respawnPos = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        capCol2D = GetComponent<CapsuleCollider2D>();
        flameBoi = transform.Find("Flame").gameObject;
        flameBoi.SetActive(false);
        col = GetComponent<Collision>();
        ani = GetComponent<Animator>();
        plrHeight = capCol2D.size.y;
        plrOffset = capCol2D.offset.y;
        spr = GetComponent<SpriteRenderer>();
        hitBoxPunch = transform.Find("PunchHitbox").gameObject;
        aud = GetComponent<Audio>();
    }

    private IEnumerator RespawnWait(float s)
    {
        yield return new WaitForSeconds(s);
        Respawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gamePaused)
        {
            UnPause();
        }
        else // (!gamePaused)
        {
            Pause();
            WallClimb();
            Movement();
            Jump();
            Punch();
            Crouch();
            Light();
            Died();
            AnimateSprite();
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = true;
        }
    }

    private void UnPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = false;
        }
    }

    private void Light()
    {
        flameBoi.SetActive(HasFlashlight);
    }

    #endregion Private Methods

    #region Internal Methods

    internal void PlayPickUp()
    {
        aud.PlayPickup();
    }

    #endregion Internal Methods

    //if(!PlayerController.gamePaused)
    //{
    //all enemy stuff in Update()
    //and set movement to 0

    //}
    //
}
