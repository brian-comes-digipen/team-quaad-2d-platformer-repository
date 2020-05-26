using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private BoxCollider2D bc;

    private float deathYValue;

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

    public bool HasFlashlight = false;

    [Header("Items")]
    public bool HasKeyBlue = false;

    public bool HasKeyRed = false;

    public bool HasKeyYellow = false;

    [Header("Other Stuff")]
    public float jumpHeight = 3f;

    public int jumpsLeft = 1;

    public GameObject punchHitbox = null;

    public int remainingLives = 3;

    public Vector2 respawnPos;

    public float respawnYValue = -5f;

    public float sprintSpeed = 5f;

    public float walkSpeed = 3f;

    #endregion Public Fields

    #region Private Methods

    private void Crouch()
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

    private IEnumerator DoubleJumpCoolDown()
    {
        jumpCooldown = true;
        yield return new WaitForSeconds(Time.deltaTime * 8);
        jumpCooldown = false;
    }

    private void Jump()
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

        if (rb2D.velocity.y < 0 && rb2D.velocity.y > -35f)
            vel.y *= 1 + .6f * Time.deltaTime * rb2D.mass;

        rb2D.velocity = vel;
    }

    private void Movement()
    {
        // Sets the RigidBody's velocity equal to our own velocity
        Vector2 vel = rb2D.velocity;

        // Left and Right Controls
        if (CanMove)
            if (Input.GetKey(KeyCode.RightArrow))
                vel.x = walkSpeed;
            else if (Input.GetKey(KeyCode.LeftArrow))
                vel.x = -walkSpeed;

        // stop it from creeping forever, adds delay when movement is 0/null
        if (Mathf.Abs(vel.x) <= walkSpeed / 7)
        {
            vel.x = 0;
        }
        else
        {
            vel.x = vel.x / 2;
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
        if (transform.position.y <= deathYValue)
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
        Respawn();
    }

    #endregion Private Methods
}
