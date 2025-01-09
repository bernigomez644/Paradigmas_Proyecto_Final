using UnityEngine;

public class RoadTile : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        // Devuelve la posición del RoadTile en el mundo
        return transform.position;
    }
}
