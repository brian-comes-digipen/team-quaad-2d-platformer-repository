using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    private SpriteRenderer spr;

    #endregion Private Fields

    #region Public Fields

    public ItemTypes itemType = ItemTypes.Dummy;

    [Header("USE THESE FIELDS ONLY FOR SPECIFIC ITEM TYPES")]
    public bool UseOnce = true;

    public bool isDestroyed = false;

    public Sprite destroyed;

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
        spr = GetComponent<SpriteRenderer>();
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
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableJump:
                        plrC.CanJump = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableDblJump:
                        plrC.CanJumpTwice = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnablePunch:
                        plrC.CanPunch = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableSprint:
                        plrC.CanSprint = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableWallClimb:
                        plrC.CanWallClimb = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableWallJump:
                        plrC.CanWallJump = true;
                        isDestroyed = true;
                        break;

                    case ItemTypes.EnableWallSlide:
                        plrC.CanWallSlide = true;
                        isDestroyed = true;
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
                        isDestroyed = true;
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
                if (isDestroyed)
                {
                    plrC.PlayPickUp();
                    this.GetComponent<SpriteRenderer>().sprite = destroyed;
                }
                if (UseOnce)
                {
                    //Destroy(gameObject);
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
