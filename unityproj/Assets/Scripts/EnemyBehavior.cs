using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;
    private Vector2 vel;

    #endregion Private Fields

    #region Public Fields

    public float moveSpeed = 1f;
    public bool onWall;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        vel = rb2D.velocity;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14) // "ENEMYBLOCK" layer
        {
            //print("Collided with block, switching direction");
            moveSpeed *= -1;
        }
        else if (collision.gameObject.layer == 15) // "PLAYERPUNCH" layer
            Destroy(gameObject);
    }

    private void Move()
    {
        if(onWall)
            vel.y = moveSpeed;
        else
            vel.x = moveSpeed;
        rb2D.velocity = vel;
    }

    #endregion Private Methods

}
