using UnityEngine;

public class EnemyDamageModifier : MonoBehaviour
{
    private float temporaryShield = 0f;  // redukce z Dark Hag

    // Přidání shieldu
    public void AddTemporaryShield(float reduction)
    {
        temporaryShield += reduction;
        if (temporaryShield > 1f) temporaryShield = 1f;
    }

    // Odebrání shieldu
    public void RemoveTemporaryShield(float reduction)
    {
        temporaryShield -= reduction;
        if (temporaryShield < 0f) temporaryShield = 0f;
    }

    // Používá se při výpočtu damage
    public int ApplyDamage(int calculatedDamage)
    {
        return Mathf.CeilToInt(calculatedDamage * (1f - temporaryShield));
    }
}

