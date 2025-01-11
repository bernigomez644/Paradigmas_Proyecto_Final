using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleFactory obstacleFactory; // Referencia a la f�brica de obst�culos
    [SerializeField] private Roads roads; // Referencia al gestor de RoadTiles
    [SerializeField] private float spawnInterval = 10f; // Intervalo de tiempo entre spawns

    private void Start()
    {
        // Inicia la rutina para generar obst�culos
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Esperar el intervalo antes de generar un nuevo obst�culo
            yield return new WaitForSeconds(spawnInterval);

            // Obtener un RoadTile aleatorio utilizando la funci�n de Roads
            Vector3 spawnPosition = roads.GetRandomPosition();

            if (spawnPosition != null)
            {
                // Generar un obst�culo en la posici�n del RoadTile
                obstacleFactory.SpawnObstacle(spawnPosition);
                Debug.Log($"Obst�culo generado en la posici�n: {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("No se encontraron RoadTiles disponibles para generar obst�culos.");
            }
        }
    }
}
