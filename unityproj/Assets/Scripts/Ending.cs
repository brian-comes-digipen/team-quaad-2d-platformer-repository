using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        win = GameObject.Find("winscreen");
        win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.GetComponent<Transform>().gameObject.layer == 15)
        {
            win.SetActive(true);
        }
    }
}
