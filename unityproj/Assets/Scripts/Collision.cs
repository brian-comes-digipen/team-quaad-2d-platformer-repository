﻿using UnityEngine;

public class Collision : MonoBehaviour
{
    #region Private Fields

    private Color debugCollisionColor = Color.red;

    #endregion Private Fields

    #region Public Fields

    public LayerMask groundLayer;

    public bool onGround;

    public bool onWall;

    public bool rightWall;

    public bool leftWall;

    public int wallSide;

    public float collisionRadius = 0.25f;

    public Vector2 groundOffset, rightOffset, leftOffset;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        rightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        leftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        //If we are on the right wall, set the value to 1. If we are on the left, set it to -1.
        wallSide = rightWall ? 1 : -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { groundOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    #endregion Private Methods
}
