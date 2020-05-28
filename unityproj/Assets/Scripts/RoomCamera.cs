using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{

    public GameObject virtualCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered Room");
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            virtualCamera.SetActive(true);
        }



    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exited Room");
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            virtualCamera.SetActive(false);

        }



    }
}
