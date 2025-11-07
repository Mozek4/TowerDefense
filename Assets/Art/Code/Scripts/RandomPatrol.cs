using UnityEngine;
using System.Collections;

public class RandomPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints; // body, kolem kterých se bude pohybovat
    [SerializeField] private float moveSpeed = 2f; // rychlost pohybu
    [SerializeField] private float wanderRadius = 2f; // poloměr náhodné pozice kolem waypointu

    private Vector3 currentTarget;

    private void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("Žádné body nejsou přiřazené!");
            return;
        }

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Vybere náhodný waypoint
            Transform basePoint = waypoints[Random.Range(0, waypoints.Length)];

            // Vygeneruje náhodnou pozici okolo něj
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            currentTarget = new Vector3(
                basePoint.position.x + randomOffset.x,
                basePoint.position.y,
                basePoint.position.z + randomOffset.y
            );

            // Určí směr pro otočení (pouze osa X)
            if (currentTarget.x > transform.position.x)
            {
                // jde doprava → otočí se o 180 na Y
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (currentTarget.x < transform.position.x)
            {
                // jde doleva → Y = 0
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            // Pohyb k cíli
            while (Vector3.Distance(transform.position, currentTarget) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    currentTarget,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }

            // Náhodná pauza mezi 5 a 12 sekundami
            float waitTime = Random.Range(5f, 12f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
