using UnityEngine;

public class TriggerColider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check player tag
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Give Player Coins");
            Destroy(gameObject);
        }
    }
}