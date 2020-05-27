using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    #endregion Private Fields

    #region Public Fields

    public ItemTypes itemType = ItemTypes.Dummy;

    #endregion Public Fields

    #region Public Enums

    public enum ItemTypes
    {
        // Null/placeholder
        Dummy = -1,

        // Abilities

        EnableCrouch,

        EnableJump,

        EnableDblJump,

        EnablePunch,

        EnableSprint,

        EnablePushPull,

        EnableWallClimb,

        // Keys & Items
        KeyBlue,

        EnableFlashlight,

        KeyRed,

        KeyYellow,

        // Other stuff
        SetRespawnPoint,

        JumpRefill,

        ExtraLife,

        Health
    }

    #endregion Public Enums

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.CanWallClimb = true;
            Destroy(gameObject);
        }
    }

    #endregion Private Methods

    #region Public Methods



    #endregion Public Methods
}
