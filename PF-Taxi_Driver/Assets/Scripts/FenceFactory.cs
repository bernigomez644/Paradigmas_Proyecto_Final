using UnityEngine;

public class FenceFactory : ObstacleFactory
{
    [SerializeField] private GameObject fencePrefab; // Prefab de la valla

    public override Obstacle SpawnObstacle(Vector3 position)
    {
        if (fencePrefab == null)
        {
            Debug.LogError("Fence prefab no asignado en el FenceFactory.");
            return null;
        }

        // Instanciar una valla en la posición indicada
        return Instantiate(fencePrefab, position, Quaternion.identity);
    }
}