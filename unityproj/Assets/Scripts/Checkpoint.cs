using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject player;
    PlayerController script;
    Animator ani;
    public Vector3 plrRespawnPos = new Vector3(0, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        script = player.GetComponent<PlayerController>();
        ani = GetComponent<Animator>();
        plrRespawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //PLAYER layer
        {
            if (script.respawnPos != plrRespawnPos)
            {
                ani.Play("Checkpoint");
                script.respawnPos = plrRespawnPos;
                print("saved position");
            }
            
        }
    }
}
