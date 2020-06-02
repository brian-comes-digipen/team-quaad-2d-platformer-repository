using System;
using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    private Animator ani;

    private Vector2 vel;

    #endregion Private Fields

    #region Public Fields

    public float moveSpeed = 1f;

    public bool onWall;

    public int health = 2;

    public float deathDelay = 0.5f;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        vel = rb2D.velocity;
        ani = GetComponent<Animator>();
        ani.SetBool("isWallEnemy", onWall);
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
            if (onWall)
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
            else
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        else if (collision.gameObject.layer == 15) // "PLAYERPUNCH" layer
            health--;

        if (health <= 0)
            StartCoroutine(TimedDeath());
    }

    private void Move()
    {
        vel.x = moveSpeed * Convert.ToInt32(!onWall);
        vel.y = moveSpeed * Convert.ToInt32(onWall);
        rb2D.velocity = vel;
    }

    private IEnumerator TimedDeath()
    {
        ani.SetBool("dead", true);
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    #endregion Private Methods
}
