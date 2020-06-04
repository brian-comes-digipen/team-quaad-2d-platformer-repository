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

    private int one = 1;
    private int two = 2;
    private int three = 3;
    private int four = 4;

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

        if (Player.GetComponent<PlayerController>().keysCollected == one)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorOne;
        }

        if (Player.GetComponent<PlayerController>().keysCollected == two)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorTwo;
        }

        if (Player.GetComponent<PlayerController>().keysCollected == three)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorThree;
        }

        if (Player.GetComponent<PlayerController>().keysCollected == four)
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorFour;
        }

        else
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorNone;
        }
    }
}
