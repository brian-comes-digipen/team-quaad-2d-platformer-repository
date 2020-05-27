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
            //Abilities
            if (itemType == ItemTypes.EnableCrouch)
            {
                controller.CanCrouch = true;
            }

            if (itemType == ItemTypes.EnableJump)
            {
                controller.CanJump = true;
            }

            if (itemType == ItemTypes.EnableDblJump)
            {
                controller.CanJumpTwice = true;
            }

            if (itemType == ItemTypes.EnablePunch)
            {
                controller.CanPunch = true;
            }

            if (itemType == ItemTypes.EnableSprint)
            {
                controller.CanSprint = true;
            }

            if (itemType == ItemTypes.EnablePushPull)
            {
                controller.CanPushPull = true;
            }

            if (itemType == ItemTypes.EnableWallClimb)
            {
                controller.CanWallClimb = true;
            }

            //Items
            if (itemType == ItemTypes.KeyBlue)
            {
                controller.HasKeyBlue = true;
            }

            if (itemType == ItemTypes.KeyRed)
            {
                controller.HasKeyRed = true;
            }

            if (itemType == ItemTypes.KeyYellow)
            {
                controller.HasKeyYellow = true;
            }

            if (itemType == ItemTypes.EnableFlashlight)
            {
                controller.HasFlashlight = true;
            }

            Destroy(gameObject);
        }
    }

    #endregion Private Methods

    #region Public Methods



    #endregion Public Methods
}
