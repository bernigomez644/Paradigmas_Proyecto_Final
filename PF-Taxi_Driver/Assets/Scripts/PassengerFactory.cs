using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> passengerPrefabs; // Lista de prefabs de pasajeros
    [SerializeField] private int poolSize = 5; // Número máximo de pasajeros
    [SerializeField] private float spawnTimer = 2f; // Intervalo de tiempo para generar pasajeros

    private GameObject[] pool; // Pool de pasajeros

    private void Awake()
    {
        PopulatePool(); // Crear el pool al iniciar el juego
    }

    private void Start()
    {
        StartCoroutine(SpawnPassengers()); // Comenzar a generar pasajeros
    }

    private void PopulatePool()
    {
        // Inicializa el pool con pasajeros desactivados
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            int randomIndex = Random.Range(0, passengerPrefabs.Count);
            pool[i] = Instantiate(passengerPrefabs[randomIndex], transform);
            pool[i].SetActive(false); // Desactiva el pasajero inicialmente
        }
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject passenger in pool)
        {
            if (!passenger.activeInHierarchy)
            {
                // Genera una posición aleatoria para el pasajero
                Vector3 spawnPosition = GetRandomPosition();
                passenger.transform.position = spawnPosition;

                // Asigna un destino aleatorio
                Vector3 destination = GetRandomPosition();
                Passenger passengerScript = passenger.GetComponent<Passenger>();
                passengerScript.SetDestination(destination);

                // Activa el pasajero
                passenger.SetActive(true);

                // Muestra información en la consola
                Debug.Log($"Pasajero creado en posición: {spawnPosition}, con destino: {destination}");
                return;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Genera una posición aleatoria dentro del mapa
        float randomX = Random.Range(-50f, 50f); // Ajusta estos valores según tu mapa
        float randomZ = Random.Range(-50f, 50f);
        return new Vector3(randomX, 0, randomZ); // Y = 0 para estar sobre el suelo
    }

    private IEnumerator SpawnPassengers()
    {
        while (true)
        {
            EnableObjectInPool(); // Activa un pasajero del pool
            yield return new WaitForSeconds(spawnTimer); // Espera antes de generar otro pasajero
        }
    }
}
