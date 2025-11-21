/* using UnityEngine;
using System.Collections.Generic;

public class MageAbility : MonoBehaviour
{
    [Header("Elemental Upgrade Unlock")]
    public GameObject elementUI; 
    public int requiredLevel = 5;

    [Header("Fire Settings")]
    public float burnDamage = 2f;
    public float burnDuration = 3f;
    public float burnTickRate = 1f;

    [Header("Ice Settings")]
    public float slowPercent = 0.4f;  // 40% slow
    public float slowDuration = 2.5f;

    [Header("Lightning Settings")]
    public float lightningRange = 4f;
    public int lightningDamage = 8;
    public LayerMask enemyLayer;

    private Turret turret;
    private ElementType selectedElement = ElementType.None;

    fireButton.onClick.AddListener(() => mageAbility.ChooseFire());
    iceButton.onClick.AddListener(() => mageAbility.ChooseIce());
    lightningButton.onClick.AddListener(() => mageAbility.ChooseLightning());


    private void Start()
    {
        turret = GetComponent<Turret>();
    }

    private void Update()
    {
        // Unlock UI only once
        if (selectedElement == ElementType.None &&
            turret != null &&
            turret.GetBpsLevel() >= requiredLevel &&
            turret.GetRangeLevel() >= requiredLevel)
        {
            if (elementUI != null)
                elementUI.SetActive(true);
        }
    }

    // --- UI Buttons ---
    public void ChooseFire()
    {
        selectedElement = ElementType.Fire;
        elementUI.SetActive(false);
    }

    public void ChooseIce()
    {
        selectedElement = ElementType.Ice;
        elementUI.SetActive(false);
    }

    public void ChooseLightning()
    {
        selectedElement = ElementType.Lightning;
        elementUI.SetActive(false);
    }

    // Called from Bullet.cs after dealing base damage
    public void ApplyElementalEffect(Transform target)
    {
        if (selectedElement == ElementType.Fire)
            ApplyBurn(target);
        else if (selectedElement == ElementType.Ice)
            ApplySlow(target);
        else if (selectedElement == ElementType.Lightning)
            CastLightning();
    }

    // --- FIRE ---
    private void ApplyBurn(Transform enemy)
    {
        BurnEffect burn = enemy.GetComponent<BurnEffect>();
        if (burn == null)
            burn = enemy.gameObject.AddComponent<BurnEffect>();

        burn.StartBurn(burnDamage, burnDuration, burnTickRate);
    }

    // --- ICE ---
    private void ApplySlow(Transform enemy)
    {
        SlowEffect slow = enemy.GetComponent<SlowEffect>();
        if (slow == null)
            slow = enemy.gameObject.AddComponent<SlowEffect>();

        slow.StartSlow(slowPercent, slowDuration);
    }

    // --- LIGHTNING ---
    private void CastLightning()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightningRange, enemyLayer);
        List<Transform> hitEnemies = new List<Transform>();

        int maxChains = 3; // kolikrát blesk může přeskákat
        Transform currentTarget = null;

        foreach (var col in enemies)
        {
            if (hitEnemies.Count >= maxChains)
                break;

            if (!hitEnemies.Contains(col.transform))
            {
                currentTarget = col.transform;
                Health hp = currentTarget.GetComponent<Health>();
                if (hp != null)
                    hp.TakeDamage(lightningDamage);

                hitEnemies.Add(currentTarget);
            }
        }
    }


public enum ElementType
{
    None,
    Fire,
    Ice,
    Lightning
} */
