using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float lifetime = 4000f; // Tiempo de vida del obst�culo en segundos

    private void Start()
    {
        // Destruir el obst�culo despu�s de su tiempo de vida
        Destroy(gameObject, lifetime);
    }
}
