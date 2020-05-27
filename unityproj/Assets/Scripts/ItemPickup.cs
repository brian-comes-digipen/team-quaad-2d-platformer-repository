using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    #endregion Private Fields

    #region Public Fields

    public ItemTypes itemType = ItemTypes.Dummy;

    [Header("USE THESE FIELDS ONLY FOR SPECIFIC ITEM TYPES")]
    public bool UseOnce = true;

    public float itemRespawnDelaySeconds = 1.0f;

    public Vector2 respawnPos = new Vector2(0, 0);

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

        Health,

        SecretCollectible
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pC = other.GetComponent<PlayerController>();

        if (pC != null)
        {
            switch (itemType)
            {
                case ItemTypes.EnableCrouch:
                    pC.CanCrouch = true;
                    break;

                case ItemTypes.EnableJump:
                    pC.CanJump = true;
                    break;

                case ItemTypes.EnableDblJump:
                    pC.CanJumpTwice = true;
                    break;

                case ItemTypes.EnablePunch:
                    pC.CanPunch = true;
                    break;

                case ItemTypes.EnableSprint:
                    pC.CanSprint = true;
                    break;

                case ItemTypes.EnablePushPull:
                    pC.CanPushPull = true;
                    break;

                case ItemTypes.EnableWallClimb:
                    pC.CanWallClimb = true;
                    break;

                case ItemTypes.KeyBlue:
                    pC.HasKeyBlue = true;
                    break;

                case ItemTypes.KeyRed:
                    pC.HasKeyRed = true;
                    break;

                case ItemTypes.KeyYellow:
                    pC.HasKeyYellow = true;
                    break;

                case ItemTypes.EnableFlashlight:
                    pC.HasFlashlight = true;
                    break;

                case ItemTypes.Health:
                    ++pC.health;
                    break;

                case ItemTypes.ExtraLife:
                    ++pC.livesLeft;
                    break;

                case ItemTypes.JumpRefill:
                    ++pC.jumpsLeft;
                    break;

                case ItemTypes.SetRespawnPoint:
                    pC.respawnPos = respawnPos;
                    break;

                default:
                    break;
            }
            if (UseOnce)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(TempDisable());
            }
        }
    }

    private IEnumerator TempDisable()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(itemRespawnDelaySeconds);
        gameObject.SetActive(true);
    }

    #endregion Private Methods
}
