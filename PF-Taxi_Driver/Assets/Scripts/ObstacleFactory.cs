using UnityEngine;

public abstract class ObstacleFactory : MonoBehaviour
{
    public abstract Obstacle SpawnObstacle(Vector3 position);
}

