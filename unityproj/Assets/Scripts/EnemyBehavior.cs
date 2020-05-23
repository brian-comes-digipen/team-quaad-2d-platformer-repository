using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D rb2D;

    #endregion Private Fields

    #region Public Fields

    public EnemyTypes enemyType = EnemyTypes.Dummy;

    #endregion Public Fields

    #region Public Enums

    public enum EnemyTypes
    {
        Dummy = -1,

        Ground,

        Wall,

        Air
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

    #endregion Private Methods
}
