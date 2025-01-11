using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSinBusqueda : MonoBehaviour
{
    public Action<string> Onchanged;
    private bool gameEnded = false; // Evitar múltiples finales del juego
    [SerializeField] private PassengerFactory passengerFactory; // Referencia a la fábrica de pasajeros
    [SerializeField] private Roads roads; // Referencia al gestor de RoadTiles
    [SerializeField] private Taxi taxi; // Referencia al Taxi en juego


    private Passenger currentPassenger; // Referencia al pasajero actual
    private RoadTile passengerTile; // Tile donde está ubicado el pasajero
    private RoadTile destinationTile; // Tile del destino del pasajero

    void Start()
    {
        // Suscribirse al evento del taxi
        taxi.OnPassengerDroppedOff += HandlePassengerDroppedOff;
        taxi.OnTaxiDestroyed += EndGame;

        // Crear el primer pasajero al iniciar el juego
        SpawnPassenger();
        ActivateTileLightForPassenger();
    }

    void Update()
    {
        // Verificar si el taxi recoge al pasajero
        CheckPassengerPickup();

        // Verificar si el taxi ha llegado al destino del pasajero
        CheckPassengerDropOff();
    }

    private void SpawnPassenger()
    {
        // Crear un pasajero usando la fábrica
        GameObject passengerObject = passengerFactory.CreatePassenger();

        if (passengerObject != null)
        {
            currentPassenger = passengerObject.GetComponent<Passenger>();
            Debug.Log($"Pasajero creado en la posición: {currentPassenger.transform.position}, con destino: {currentPassenger.destination}");
        }
        else
        {
            Debug.LogError("No se pudo crear un pasajero.");
        }
    }

    private void ActivateTileLightForPassenger()
    {
        if (currentPassenger == null)
        {
            Debug.LogError("No hay un pasajero para identificar su posición.");
            return;
        }

        // Obtener el tile en el que se encuentra el pasajero
        passengerTile = roads.GetRoadTileAtPosition(currentPassenger.transform.position);

        if (passengerTile != null)
        {
            Debug.Log($"Activando luz en el tile de posición: {passengerTile.GetPosition()}");
            passengerTile.SetLightActive(true); // Activar la luz en el tile
        }
        else
        {
            Debug.LogError("No se encontró un RoadTile en la posición del pasajero.");
        }
    }

    private void ActivateTileLightForDestination()
    {
        if (currentPassenger == null)
        {
            Debug.LogError("No hay un pasajero para identificar su destino.");
            return;
        }

        // Obtener el tile correspondiente al destino del pasajero
        destinationTile = roads.GetRoadTileAtPosition(currentPassenger.destination);

        if (destinationTile != null)
        {
            Debug.Log($"Activando luz en el tile de destino: {destinationTile.GetPosition()}");
            destinationTile.SetLightActive(true); // Activar la luz en el tile de destino
        }
        else
        {
            Debug.LogError("No se encontró un RoadTile en la posición del destino del pasajero.");
        }
    }

    private void DeactivateTileLightForDestination()
    {
        if (destinationTile != null)
        {
            Debug.Log($"Apagando luz en el tile de destino: {destinationTile.GetPosition()}");
            destinationTile.SetLightActive(false); // Apagar la luz en el tile de destino
        }
    }

    private void CheckPassengerPickup()
    {
        if (currentPassenger == null || taxi == null)
        {
            return;
        }

        // Calcular la distancia entre el taxi y el pasajero
        float distance = Vector3.Distance(taxi.transform.position, currentPassenger.transform.position);

        if (distance < 3f) // Si el taxi está a menos de 3 unidades
        {
            Onchanged?.Invoke("Pasajero recogido.");
            currentPassenger.gameObject.SetActive(false); // Desactivar al pasajero
            passengerTile.SetLightActive(false); // Apagar la luz del tile del pasajero
            ActivateTileLightForDestination(); // Activar la luz en el tile del destino
            taxi.PickUpPassenger(); // Activar la variable del taxi indicando que lleva un pasajero
        }
    }

    private void CheckPassengerDropOff()
    {
        if (!taxi.HasPassenger() || currentPassenger == null)
        {
            return;
        }

        // Calcular la distancia entre el taxi y el destino del pasajero
        float distanceToDestination = Vector3.Distance(taxi.transform.position, currentPassenger.destination);

        if (distanceToDestination < 8f) // Si el taxi está a menos de 3 unidades del destino
        {
            Debug.Log("Pasajero dejado en su destino.");
            Onchanged?.Invoke("Pasajero entregado, +5 de vida.");
            DeactivateTileLightForDestination(); // Apagar la luz del destino
            taxi.DropOffPassenger(); // Indicar que el taxi ya no lleva pasajero
        }
    }

    private void HandlePassengerDroppedOff()
    {
        Debug.Log("Evento: Pasajero dejado, generando un nuevo pasajero.");
        // Generar un nuevo pasajero
        SpawnPassenger();
        ActivateTileLightForPassenger();
        taxi.HealthUP(5f);
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar problemas
        taxi.OnPassengerDroppedOff -= HandlePassengerDroppedOff;
    }

    public void EndGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        // Mostrar "Game Over" en la UI
        Onchanged?.Invoke("Game Over");

        // Iniciar la corrutina para salir del "game mode"
        StartCoroutine(StopGameAfterDelay(3f));
    }

    private IEnumerator StopGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 0; // Detener el tiempo del juego
        Debug.Log("Game Over. El juego ha terminado.");


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detener el modo de juego en el Editor
#else
        Debug.Log("Game has ended, but the application remains open.");
#endif
    }
}

