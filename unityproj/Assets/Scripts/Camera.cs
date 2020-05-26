using UnityEngine;

public class Camera : MonoBehaviour
{
    #region Public Fields

    public GameObject cineCam;

    #endregion Public Fields

    #region Private Methods

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player") && !trigger.isTrigger)
        {
            cineCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player") && !trigger.isTrigger)
        {
            cineCam.SetActive(false);
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
