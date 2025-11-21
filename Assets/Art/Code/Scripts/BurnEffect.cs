/* using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    private float tickRate;
    private float tickTimer;
    private int damagePerTick;
    private float duration;
    private float timer;

    private Health health;

    public void StartBurn(float dmg, float dur, float rate)
    {
        health = GetComponent<Health>();
        damagePerTick = Mathf.RoundToInt(dmg);
        duration = dur;
        tickRate = rate;
        tickTimer = 0;
        timer = duration;
    }

    private void Update()
    {
        if (timer <= 0) 
        {
            Destroy(this);
            return;
        }

        timer -= Time.deltaTime;
        tickTimer += Time.deltaTime;

        if (tickTimer >= tickRate)
        {
            tickTimer = 0;
            health.TakeDamage(damagePerTick);
        }
    }
} */
