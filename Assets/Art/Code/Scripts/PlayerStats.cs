using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("Tower Multipliers")]
    public float towerDamageMultiplier = 1f;
    public float towerRangeMultiplier = 1f;
    public float towerAttackSpeedMultiplier = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
