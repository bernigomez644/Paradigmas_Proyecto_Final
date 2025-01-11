using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleFactory obstacleFactory; // Referencia a la fábrica de obstáculos
    [SerializeField] private Roads roads; // Referencia al gestor de RoadTiles
    [SerializeField] private float spawnInterval = 10f; // Intervalo de tiempo entre spawns

    private void Start()
    {
        // Inicia la rutina para generar obstáculos
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Esperar el intervalo antes de generar un nuevo obstáculo
            yield return new WaitForSeconds(spawnInterval);

            // Obtener un RoadTile aleatorio utilizando la función de Roads
            Vector3 spawnPosition = roads.GetRandomPosition();

            if (spawnPosition != null)
            {
                // Generar un obstáculo en la posición del RoadTile
                obstacleFactory.SpawnObstacle(spawnPosition);
                Debug.Log($"Obstáculo generado en la posición: {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("No se encontraron RoadTiles disponibles para generar obstáculos.");
            }
        }
    }
}
