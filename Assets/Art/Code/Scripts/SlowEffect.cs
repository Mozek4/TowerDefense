/* using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    private float slowAmount;
    private float duration;
    private float timer;

    private EnemyMovement movement;

    public void StartSlow(float amount, float dur)
    {
        movement = GetComponent<EnemyMovement>();
        slowAmount = amount;
        duration = dur;
        timer = duration;

        movement.UpdateSpeed(movement.GetBaseSpeed() * (1f - slowAmount));
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            movement.ResetSpeed();
            Destroy(this);
        }
    }
} */
