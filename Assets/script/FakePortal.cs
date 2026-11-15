using System.Collections;
using UnityEngine;

public class FakePortal : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition = new Vector2(-8.32f, -2.15f);
    [SerializeField] private float animationDuration = 0.25f;

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithAnimation(other.gameObject));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTeleporting && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithAnimation(collision.gameObject));
        }
    }

    private IEnumerator TeleportWithAnimation(GameObject player)
    {
        isTeleporting = true;

        Vector3 originalScale = player.transform.localScale;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        // Freeze physics while shrinking into the portal
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // 1. Shrink the player into the portal
        float timer = 0f;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            player.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, timer / animationDuration);
            yield return null;
        }

        // 2. Teleport to (-8.32, -2.15)
        player.transform.position = new Vector3(targetPosition.x, targetPosition.y, player.transform.position.z);

        // 3. Grow the player back to normal size at the start
        timer = 0f;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            player.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer / animationDuration);
            yield return null;
        }

        player.transform.localScale = originalScale;

        // Re-enable physics
        if (rb != null)
        {
            rb.simulated = true;
        }

        isTeleporting = false;
    }
}