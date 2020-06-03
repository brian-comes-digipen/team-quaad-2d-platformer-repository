using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite Opened;

    public int DoorValue = 0;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerController>().KeysCollected == doorValue)
        {
            this.GetComponent<SpriteRenderer>().sprite = Opened;
        }
    }
}
