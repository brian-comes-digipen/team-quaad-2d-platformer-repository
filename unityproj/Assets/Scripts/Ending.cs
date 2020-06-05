using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        win = GameObject.Find("screenwin");
        win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            print("collided");
            win.SetActive(true);
        }
    }
}
