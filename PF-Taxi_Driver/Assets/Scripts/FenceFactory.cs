using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceFactory : ObstacleFactory
{
    [SerializeField] private GameObject fencePrefab; // Asegúrate de que sea un GameObject

    public override Obstacle SpawnObstacle(Vector3 position)
    {
        // Instanciar el prefab
        GameObject instantiatedFence = Instantiate(fencePrefab, position, Quaternion.identity);

        // Obtener el componente del tipo Obstacle
        Obstacle fence = instantiatedFence.GetComponent<Obstacle>();

        if (fence == null)
        {
            Debug.LogError("El prefab asignado no contiene un componente de tipo Obstacle.");
        }

        return fence;
    }
}
