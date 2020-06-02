using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileKill : MonoBehaviour
{
    #region Private Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController plrC = collision.gameObject.GetComponent<PlayerController>();
            plrC.gameObject.transform.position = plrC.respawnPos;
            print("Player collided with designated out-of-bounds tilemap, resetting player position to respawn position.");
        }
    }

    #endregion Private Methods
}
