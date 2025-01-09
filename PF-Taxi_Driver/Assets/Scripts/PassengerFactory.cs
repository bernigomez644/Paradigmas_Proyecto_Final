using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    // Singleton: Instancia estática de PassengerFactory
    public static PassengerFactory Instance { get; private set; }

    [SerializeField] private List<GameObject> passengerPrefabs; // Lista de prefabs de pasajeros
    [SerializeField] private Roads roads; // Referencia al script Roads que contiene los RoadTiles

    private void Awake()
    {
        // Verificar si ya existe una instancia del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignar la instancia única
        Instance = this;
    }

    public GameObject CreatePassenger()
    {
        int randomIndex = Random.Range(0, passengerPrefabs.Count);
        GameObject passenger = Instantiate(passengerPrefabs[randomIndex], transform);

        // Generar posición inicial y destino desde los RoadTiles
        Vector3 spawnPosition = roads.GetRandomPosition();
        Vector3 destination = roads.GetRandomPosition();

        // Asegurarse de que el destino sea diferente del origen
        while (spawnPosition == destination)
        {
            destination = roads.GetRandomPosition();
        }

        passenger.transform.position = spawnPosition;
        Passenger passengerScript = passenger.GetComponent<Passenger>();
        passengerScript.SetDestination(destination);

        Debug.Log($"Pasajero creado en posición: {spawnPosition}, con destino: {destination}");

        return passenger;
    }
}
