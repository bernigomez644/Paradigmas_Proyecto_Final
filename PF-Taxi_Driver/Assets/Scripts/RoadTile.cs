using UnityEngine;

public class RoadTile : MonoBehaviour
{
    private Light tileLight; // Referencia al componente Light
    private MeshCollider meshCollider; // Referencia al MeshCollider

    private void Awake()
    {
        // Obtener el componente Light directamente en el objeto
        tileLight = GetComponent<Light>();

        // Obtener el MeshCollider del objeto (si existe)
        meshCollider = GetComponent<MeshCollider>();
    }

    /// <summary>
    /// Activa o desactiva la luz del RoadTile.
    /// </summary>
    public void SetLightActive(bool active)
    {
        if (tileLight != null)
        {
            tileLight.enabled = active;
        }
    }

    /// <summary>
    /// Devuelve la posición del RoadTile en el mundo.
    /// </summary>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Devuelve el MeshCollider asociado al RoadTile (si existe).
    /// </summary>
    public MeshCollider GetMeshCollider()
    {
        return meshCollider;
    }
}



