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

    public Vector2 plrRespawnPos = new Vector2(0, 0);

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

        EnableWallClimb,

        EnableWallJump,

        EnableWallSlide,

        // Keys & Items
        EnableFlashlight,

        KeyBlue,

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
        if (other.GetComponentInParent<PlayerController>() != null && other.GetComponent<Transform>().gameObject.layer == 15)
        {
            PlayerController plrC = other.GetComponentInParent<PlayerController>();

            if (plrC != null)
            {
                switch (itemType)
                {
                    case ItemTypes.EnableCrouch:
                        plrC.CanCrouch = true;
                        break;

                    case ItemTypes.EnableJump:
                        plrC.CanJump = true;
                        break;

                    case ItemTypes.EnableDblJump:
                        plrC.CanJumpTwice = true;
                        break;

                    case ItemTypes.EnablePunch:
                        plrC.CanPunch = true;
                        break;

                    case ItemTypes.EnableSprint:
                        plrC.CanSprint = true;
                        break;

                    case ItemTypes.EnableWallClimb:
                        plrC.CanWallClimb = true;
                        break;

                    case ItemTypes.EnableWallJump:
                        plrC.CanWallJump = true;
                        break;

                    case ItemTypes.EnableWallSlide:
                        plrC.CanWallSlide = true;
                        break;

                    case ItemTypes.KeyBlue:
                        plrC.HasKeyBlue = true;
                        break;

                    case ItemTypes.KeyRed:
                        plrC.HasKeyRed = true;
                        break;

                    case ItemTypes.KeyYellow:
                        plrC.HasKeyYellow = true;
                        break;

                    case ItemTypes.EnableFlashlight:
                        plrC.HasFlashlight = true;
                        break;

                    case ItemTypes.Health:
                        ++plrC.health;
                        break;

                    case ItemTypes.ExtraLife:
                        ++plrC.livesLeft;
                        break;

                    case ItemTypes.JumpRefill:
                        ++plrC.jumpsLeft;
                        break;

                    case ItemTypes.SetRespawnPoint:
                        plrC.respawnPos = plrRespawnPos;
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
    }

    private IEnumerator TempDisable()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(itemRespawnDelaySeconds);
        gameObject.SetActive(true);
    }

    #endregion Private Methods
}
