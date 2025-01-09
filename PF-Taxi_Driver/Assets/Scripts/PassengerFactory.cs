using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    // Singleton: Instancia estática de PassengerFactory
    public static PassengerFactory Instance { get; private set; }

    [SerializeField] private List<GameObject> passengerPrefabs; // Lista de prefabs de pasajeros

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

        // Generar posición inicial y destino
        Vector3 spawnPosition = GetRandomPosition();
        Vector3 destination = GetRandomPosition();

        passenger.transform.position = spawnPosition;
        Passenger passengerScript = passenger.GetComponent<Passenger>();
        passengerScript.SetDestination(destination);

        Debug.Log($"Pasajero creado en posición: {spawnPosition}, con destino: {destination}");

        return passenger;
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-50f, 50f); // Ajusta estos valores según tu mapa
        float randomZ = Random.Range(-50f, 50f);
        return new Vector3(randomX, 0, randomZ); // Altura Y = 0
    }
}
