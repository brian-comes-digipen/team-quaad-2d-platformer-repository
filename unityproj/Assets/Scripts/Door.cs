using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;

    public GameObject EnterTrigger;

    public Sprite DoorOpened;

    public Sprite DoorOne;

    public Sprite DoorTwo;

    public Sprite DoorThree;

    public Sprite DoorFour;

    public Sprite DoorNone;

    public int DoorValue = 4;

    // Start is called before the first frame update
    void Awake()
    {
        EnterTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerController>().keysCollected == 1)
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
            TorchLights.lightsOff = true;
            EnterTrigger.SetActive(true);
        }

        else
        {
            this.GetComponent<SpriteRenderer>().sprite = DoorNone;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something Entered");
        if (other.GetComponentInParent<PlayerController>() != null && other.GetComponent<Transform>().gameObject.layer == 15)
        {
            Debug.Log("Player Entered");
            if (Player.GetComponent<PlayerController>().keysCollected == DoorValue)
            {
                Debug.Log("Door Opened");
                this.GetComponent<SpriteRenderer>().sprite = DoorOpened;
            }
        }
    }
}
