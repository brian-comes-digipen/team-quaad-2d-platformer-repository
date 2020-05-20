using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private bool isDead;

    private Rigidbody2D rb2D;

    private Vector3 savePos;

    private GameObject text;

    #endregion Private Fields

    #region Public Fields

    public int jumpCount = 1;

    public float jumpHeight = 3f;

    public float moveSpeed = 3f;

    public Vector3 vel;

    public float weight = 0.5f;

    #endregion Public Fields

    #region Private Methods

    private void Jump()
    {
        vel = rb2D.velocity;

        //Jump controls, implements double jump restraints by decreasing the "counter" each use
        if (Input.GetKeyDown(KeyCode.UpArrow) && (jumpCount > 0))
        {
            //Height is multiplied to compensate for the increase of negative velocity
            vel.y = jumpHeight * 1.4f;
            jumpCount--;
        }

        //When the player touches a layer (ground) the double jump cap is reset
        else if (rb2D.velocity.y == 0 && rb2D.IsTouchingLayers())
        {
            jumpCount = 2;

            //Resets the height to its original value to prevent a constant height increase
        }

        //Increases down-ward momentum to simulate weight
        if (rb2D.velocity.y < 0 && rb2D.velocity.y > -35f)
        {
            vel.y *= 1 + (.6f * Time.deltaTime) * weight;

            //myRB.velocity += Vector2.up * Physics2D.gravity.y * (weight - 1) * Time.deltaTime;
        }

        rb2D.velocity = vel;
    }

    private void Movement()
    {
        //Sets the RigidBody's velocity equal to our own velocity
        vel = rb2D.velocity;

        //Left and Right Controls
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -moveSpeed;
        }

        //Sets velocity to 0 if controls are not being used
        else
        {
            // stop it from creeping forever, adds delay when movement is 0/null
            if (Mathf.Abs(vel.x) < moveSpeed / 7)
            {
                vel.x = 0;
            }
            else
            {
                vel.x = vel.x / 2;
            }
        }

        //Sets our own velocity equal to the value of the Rigidbody velocity
        rb2D.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ah")
        {
            Destroy(collision.gameObject);
            text.transform.position = new Vector3(0, 0);
        }
    }

    private void Respawn()
    {
        if (isDead)
        {
            this.transform.position = savePos;
            isDead = false;
        }
        if (transform.position.y < -5)
        {
            isDead = true;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        savePos = this.transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        text = GameObject.Find("text");
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        Jump();
        Respawn();
    }

    #endregion Private Methods
}
