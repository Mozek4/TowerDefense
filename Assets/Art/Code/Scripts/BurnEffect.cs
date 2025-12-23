using System.Collections;
using UnityEngine;

public class FireBurn : MonoBehaviour
{
    private int damage;
    private float interval;
    private int ticks;
    private Health health;

    public void Init(int dmg, float time, int count)
    {
        damage = dmg;
        interval = time;
        ticks = count;
        health = GetComponent<Health>();
        Debug.Log("Burn applied: " + damage + " dmg, " + ticks + " ticks");


        StartCoroutine(BurnCoroutine());
    }

    private IEnumerator BurnCoroutine()
    {
        for (int i = 0; i < ticks; i++)
        {
            if (health == null) break;
            Debug.Log("Burn tick");

            health.TakeDamage(damage);
            yield return new WaitForSeconds(interval);
        }

        Destroy(this);
    }
}
