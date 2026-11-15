using UnityEngine;
using UnityEngine.SceneManagement;

public class RealPortal : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "Level2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Real Portal touched! Loading " + nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
    }
}