/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ElementType { None, Fire, Ice, Lightning }

public class MageAbility : MonoBehaviour
{
    [Header("Elemental Upgrade Unlock")]
    public GameObject elementUI;
    public Button fireButton;
    public Button iceButton;
    public Button lightningButton;
    public int requiredLevel = 5;

    [Header("Fire Settings")]
    public int burnDamagePerTick = 2;
    public float burnDuration = 3f;
    public float burnTickRate = 1f;

    [Header("Ice Settings")]
    [Range(0f, 1f)] public float slowPercent = 0.4f;  // 40%
    public float slowDuration = 2.5f;

    [Header("Lightning Settings")]
    public float lightningRange = 4f;
    public int lightningDamage = 8;
    public LayerMask enemyLayer;
    public int lightningMaxChains = 3;

    private Turret turret;
    private ElementType selectedElement = ElementType.None;

    // správa běžících efektů (už nekomponenty na enemym)
    private Dictionary<Transform, Coroutine> burnCoroutines = new Dictionary<Transform, Coroutine>();
    private Dictionary<Transform, Coroutine> slowCoroutines = new Dictionary<Transform, Coroutine>();

    private void Start()
    {
        turret = GetComponent<Turret>();

        if (fireButton != null) fireButton.onClick.AddListener(ChooseFire);
        if (iceButton != null) iceButton.onClick.AddListener(ChooseIce);
        if (lightningButton != null) lightningButton.onClick.AddListener(ChooseLightning);

        if (elementUI != null) elementUI.SetActive(false);
    }

    private void Update()
    {
        // odemčení UI jednou, pokud turret dosáhl úrovní
        if (selectedElement == ElementType.None && turret != null)
        {
            // vyžaduje, aby Turret měl veřejné GetBpsLevel/GetRangeLevel
            if (turret.GetBpsLevel() >= requiredLevel && turret.GetRangeLevel() >= requiredLevel)
            {
                if (elementUI != null && !elementUI.activeSelf)
                    elementUI.SetActive(true);
            }
        }
    }

    // --- UI volání ---
    public void ChooseFire()
    {
        selectedElement = ElementType.Fire;
        if (elementUI != null) elementUI.SetActive(false);
    }

    public void ChooseIce()
    {
        selectedElement = ElementType.Ice;
        if (elementUI != null) elementUI.SetActive(false);
    }

    public void ChooseLightning()
    {
        selectedElement = ElementType.Lightning;
        if (elementUI != null) elementUI.SetActive(false);
    }

    // Volá Bullet po zásahu (počítá se, že base damage už byl aplikován)
    public void ApplyElementalEffect(Transform target)
    {
        if (target == null) return;

        switch (selectedElement)
        {
            case ElementType.Fire:
                ApplyBurn(target);
                break;
            case ElementType.Ice:
                ApplySlow(target);
                break;
            case ElementType.Lightning:
                CastLightning(target);
                break;
            case ElementType.None:
            default:
                break;
        }
    }

    // --- FIRE (burn) ---
    private void ApplyBurn(Transform enemy)
    {
        // Refresh nebo start nového coroutine -> efekt se obnoví
        if (burnCoroutines.ContainsKey(enemy))
        {
            StopCoroutine(burnCoroutines[enemy]);
            burnCoroutines.Remove(enemy);
        }

        Coroutine c = StartCoroutine(BurnCoroutine(enemy));
        burnCoroutines.Add(enemy, c);
    }

    private IEnumerator BurnCoroutine(Transform enemy)
    {
        Health h = enemy.GetComponent<Health>();
        float timer = burnDuration;
        float tickTimer = 0f;

        while (enemy != null && timer > 0f)
        {
            timer -= Time.deltaTime;
            tickTimer += Time.deltaTime;

            if (tickTimer >= burnTickRate)
            {
                tickTimer = 0f;
                if (h != null)
                    h.TakeDamage(burnDamagePerTick);
            }

            yield return null;
        }

        if (burnCoroutines.ContainsKey(enemy))
            burnCoroutines.Remove(enemy);
    }

    // --- ICE (slow) ---
    private void ApplySlow(Transform enemy)
    {
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement == null) return;

        // refresh existujícího slowu
        if (slowCoroutines.ContainsKey(enemy))
        {
            StopCoroutine(slowCoroutines[enemy]);
            slowCoroutines.Remove(enemy);
            // vrátíme se na základní rychlost a znovu nastartujeme
            movement.ResetSpeed();
        }

        Coroutine c = StartCoroutine(SlowCoroutine(enemy));
        slowCoroutines.Add(enemy, c);
    }

    private IEnumerator SlowCoroutine(Transform enemy)
    {
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement == null)
        {
            slowCoroutines.Remove(enemy);
            yield break;
        }

        // store base and apply slow
        float baseSpeed = movement.GetBaseSpeed();
        movement.UpdateSpeed(baseSpeed * (1f - slowPercent));

        float timer = slowDuration;
        while (enemy != null && timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // restore
        if (movement != null)
            movement.ResetSpeed();

        if (slowCoroutines.ContainsKey(enemy))
            slowCoroutines.Remove(enemy);
    }

    // --- LIGHTNING (instant chain) ---
    private void CastLightning(Transform primaryTarget)
    {
        // jednoduchá implementace: najdeme všechna okolní nepřátele a utrhneme až lightningMaxChains
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightningRange, enemyLayer);
        if (enemies == null || enemies.Length == 0) return;

        // seřaď podle vzdálenosti (nejblíže turretu)
        System.Array.Sort(enemies, (a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position))
        );

        List<Transform> hit = new List<Transform>();
        for (int i = 0; i < enemies.Length && hit.Count < lightningMaxChains; i++)
        {
            Transform t = enemies[i].transform;
            if (!hit.Contains(t))
            {
                Health hp = t.GetComponent<Health>();
                if (hp != null)
                    hp.TakeDamage(lightningDamage);

                hit.Add(t);
            }
        }

        // (volitelně přidat VFX/LineRenderer pro blesk)
    }

    // Public getter pro debug/inspektor
    public ElementType GetSelectedElement() => selectedElement;
} */

