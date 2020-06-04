using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;

    public Sprite DoorOpened;

    public Sprite DoorOne;

    public Sprite DoorTwo;

    public Sprite DoorThree;

    public Sprite DoorFour;

    public Sprite DoorNone;

    public int DoorValue = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerController>().keysCollected == DoorValue)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorOpened;
        }

        else if (Player.GetComponent<PlayerController>().keysCollected == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorOne;
        }

        else if (Player.GetComponent<PlayerController>().keysCollected == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorTwo;
        }

        else if (Player.GetComponent<PlayerController>().keysCollected == 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorThree;
        }

        else if (Player.GetComponent<PlayerController>().keysCollected == 4)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorFour;
        }

        else
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorNone;
        }
    }
}
