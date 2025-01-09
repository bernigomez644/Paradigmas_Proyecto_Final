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
}

