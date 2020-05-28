using UnityEngine;

public class Camera : MonoBehaviour
{
    #region Public Fields

    public GameObject virtualCam;

    #endregion Public Fields

    #region Private Methods

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered");
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            
            virtualCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exited");
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            
            virtualCam.SetActive(false);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    #endregion Private Methods
}
