using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerController player;
    Animator ani;
    public Vector2 plrRespawnPos = new Vector2(0, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        ani = GetComponent<Animator>();
        plrRespawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //PLAYER layer
        {
            player.respawnPos = plrRespawnPos;
            print("saved position");
        }
    }
}
