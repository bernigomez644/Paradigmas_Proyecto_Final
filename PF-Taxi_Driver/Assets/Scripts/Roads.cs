using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour
{
    private List<RoadTile> roadTiles = new List<RoadTile>();

    private void Awake()
    {
        InitializeRoadTiles();
    }

    private void InitializeRoadTiles()
    {
        // Busca todos los RoadTiles en los hijos de este GameObject
        foreach (Transform child in transform)
        {
            RoadTile roadTile = child.GetComponent<RoadTile>();
            if (roadTile != null)
            {
                roadTiles.Add(roadTile);
            }
        }

        // Verificar que se hayan encontrado RoadTiles
        if (roadTiles.Count == 0)
        {
            Debug.LogWarning("No se encontraron RoadTiles en el GameObject Road.");
        }
    }

    public Vector3 GetRandomPosition()
    {
        // Devuelve la posición de un RoadTile aleatorio
        if (roadTiles.Count > 0)
        {
            int randomIndex = Random.Range(0, roadTiles.Count);
            return roadTiles[randomIndex].GetPosition();
        }

        Debug.LogError("No hay RoadTiles disponibles para obtener una posición.");
        return Vector3.zero;
    }

    public List<RoadTile> GetRoadTiles()
    {
        return roadTiles;
    }

    public RoadTile GetRoadTileAtPosition(Vector3 position)
    {
        RoadTile closestTile = null;
        float closestDistance = Mathf.Infinity;

        foreach (RoadTile roadTile in roadTiles)
        {
            // Verificar si el roadTile tiene un collider inicializado
            if (roadTile.GetMeshCollider() != null && roadTile.GetMeshCollider().bounds.Contains(position))
            {
                return roadTile; // Devuelve directamente si la posición está dentro del bounds del collider
            }

            // Si no está en los bounds, calcular distancia como respaldo
            float distance = Vector3.Distance(position, roadTile.transform.position);
            if (distance < closestDistance)
            {
                closestTile = roadTile;
                closestDistance = distance;
            }
        }


        return closestTile; // Devuelve el más cercano si no se encuentra uno en los bounds
    }

}

