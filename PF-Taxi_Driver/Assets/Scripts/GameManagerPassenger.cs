//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] private Taxi taxi; // Referencia al Taxi
//    [SerializeField] private Roads roads; // Referencia al Roads para obtener los RoadTiles
//    [SerializeField] private PassengerFactory passengerFactory; // Referencia a la PassengerFactory

//    private Passenger currentPassenger; // Referencia al pasajero actual
//    private List<RoadTile> highlightedTiles = new List<RoadTile>(); // Lista de tiles resaltados actualmente

//    private void Start()
//    {
//        // Crear el primer pasajero al iniciar el juego
//        SpawnNewPassenger();
//    }

//    public void OnPassengerReachedDestination()
//    {
//        // El pasajero actual ha llegado a su destino
//        ClearHighlightedTiles();

//        // Crear un nuevo pasajero
//        SpawnNewPassenger();
//    }

//    private void SpawnNewPassenger()
//    {
//        GameObject passengerObject = passengerFactory.CreatePassenger();

//        if (passengerObject != null)
//        {
//            currentPassenger = passengerObject.GetComponent<Passenger>();

//            // Calcular el camino al nuevo pasajero
//            HighlightPathToPassenger();
//        }
//    }

//    private void HighlightPathToPassenger()
//    {
//        if (currentPassenger == null) return;

//        RoadTile tile = roads.GetRoadTileAtPosition(taxi.transform.position);
//        Debug.Log($"el tile del taxi me lo pilla? {tile.transform.position}");
//        RoadTile tile2 = roads.GetRoadTileAtPosition(currentPassenger.transform.position);
//        Debug.Log($"el tile del pasajero me lo pilla? {tile2.transform.position}");

//        // Calcular el camino más corto
//        List<RoadTile> path = FindPath(tile, tile2);

//        if (path.Count > 0)
//        {
//            Debug.Log("Este es el path encontrado:");
//            foreach (RoadTile til in path)
//            {
//                Debug.Log($"Tile en posición: {til.GetPosition()}");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("No se encontró un path válido.");
//        }

//        // Activar los halos en los tiles del camino
//        foreach (var til in path)
//        {
//            tile.SetHaloActive(true); // Activar el halo del tile
//            highlightedTiles.Add(tile); // Guardar referencia para limpiar después
//        }
//    }

//    private void ClearHighlightedTiles()
//    {
//        // Desactivar los halos de los tiles previamente iluminados
//        foreach (var tile in highlightedTiles)
//        {
//            tile.SetHaloActive(false); // Desactivar el halo
//        }

//        highlightedTiles.Clear();
//    }

//    public List<RoadTile> FindPath(RoadTile startTile, RoadTile endTile)
//    {
//        Queue<RoadTile> frontier = new Queue<RoadTile>(); // Cola para la búsqueda
//        Dictionary<RoadTile, RoadTile> cameFrom = new Dictionary<RoadTile, RoadTile>(); // Para reconstruir el camino
//        frontier.Enqueue(startTile);
//        cameFrom[startTile] = null;

//        while (frontier.Count > 0)
//        {
//            RoadTile currentTile = frontier.Dequeue();

//            // Si llegamos al destino, reconstruimos el camino
//            if (currentTile == endTile)
//            {
//                List<RoadTile> path = new List<RoadTile>();
//                while (currentTile != startTile)
//                {
//                    path.Insert(0, currentTile); // Insertamos al principio para invertir el camino
//                    currentTile = cameFrom[currentTile];
//                }
//                path.Insert(0, startTile); // Insertamos el punto de inicio

//                Debug.Log("Path calculado correctamente:");
//                foreach (var tile in path)
//                {
//                    Debug.Log($"Tile en posición: {tile.GetPosition()}");
//                }

//                return path; // Devolver la lista de tiles del camino
//            }

//            // Recorremos los vecinos de los tiles
//            foreach (RoadTile neighbor in GetNeighbors(currentTile)) // Llama a un método para obtener vecinos
//            {
//                if (!cameFrom.ContainsKey(neighbor)) // Si no hemos visitado este tile
//                {
//                    frontier.Enqueue(neighbor);
//                    cameFrom[neighbor] = currentTile;
//                }
//            }
//        }

//        Debug.LogWarning("No se encontró un camino válido entre los tiles.");
//        return new List<RoadTile>(); // Devolver una lista vacía si no hay camino
//    }

//    private List<RoadTile> GetNeighbors(RoadTile tile)
//    {
//        List<RoadTile> neighbors = new List<RoadTile>();

//        // Recorremos todos los tiles disponibles en el mapa
//        foreach (RoadTile potentialNeighbor in roads.GetRoadTiles())
//        {
//            // Consideramos vecinos los tiles que están dentro de un rango (ajusta este valor según tu diseño)
//            if (tile != potentialNeighbor && Vector3.Distance(tile.GetPosition(), potentialNeighbor.GetPosition()) < 1.5f)
//            {
//                neighbors.Add(potentialNeighbor);
//            }
//        }

//        return neighbors;
//    }



//}

