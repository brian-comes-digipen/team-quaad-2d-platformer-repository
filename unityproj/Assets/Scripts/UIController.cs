using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Public Fields

    public GameObject Player;

    private PlayerController Controller;

    public int numOfHearts;

    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.Find("Player");
        Controller = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Controller.health > numOfHearts)
        {
            Controller.health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Controller.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // How many hearts on screen
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void EditPlayerHealth(int health)
    {
        Controller.health += health;

        return;
    }

    #endregion Private Methods
}
