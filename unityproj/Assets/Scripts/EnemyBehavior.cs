using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    #endregion Private Fields

    #region Public Fields

    public float moveSpeed = 1f;
    public Vector3 velocity;
    public bool onWall;

    #endregion Public Fields

    #region Public Enums
    #endregion Public Enums

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        velocity = rb2D.velocity;
    }

    // Update is called once per frame
    private void Update()
    {
        Ground();
        Wall();
    }
    

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy Wall")
        {
            //print("Collided");
            moveSpeed = -moveSpeed;
        }
    }

    private void Ground()
    {
        if(!onWall)
        {
            velocity.x = moveSpeed;
            rb2D.velocity = velocity;
        }
    }

    private void Wall()
    {
        if (onWall)
        {
            rb2D.gravityScale = 0;
            velocity.y = moveSpeed;
            rb2D.velocity = velocity;
            print("test");
        }
    }

    #endregion Private Methods

}
