using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Public Fields

    public PlayerController Player;

    public int numOfHearts;

    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite halfHeart;

    public Sprite emptyHeart;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Player.health > numOfHearts)
        {
            Player.health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Player.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // How many hearts on screen
            if (i < (numOfHearts - 3))
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    #endregion Private Methods
}
