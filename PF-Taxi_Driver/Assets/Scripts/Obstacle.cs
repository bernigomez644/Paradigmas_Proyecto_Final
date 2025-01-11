using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float lifetime = 4000f; // Tiempo de vida del obstáculo en segundos

    private void Start()
    {
        // Destruir el obstáculo después de su tiempo de vida
        Destroy(gameObject, lifetime);
    }
}
