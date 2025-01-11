using System;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public Vector3 destination; // Destino del pasajero
    public static event Action OnPassengerReachedDestination; // Evento que notifica al llegar al destino

    public bool isWaiting = true; // Indica si el pasajero está esperando
    private Light waitingLight;  // Luz que representa al pasajero esperando

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
        Debug.Log($"Destino asignado al pasajero: {destination}");
    }

    private void Start()
    {
        // Crear la luz para representar al pasajero esperando
        waitingLight = gameObject.AddComponent<Light>();
        waitingLight.type = LightType.Spot; // Tipo de luz punto para marcar solo la posición
        waitingLight.range = 10; // Alcance corto para indicar la posición exacta
        waitingLight.intensity = 6; // Intensidad adecuada para hacerlo visible
        waitingLight.color = Color.blue; // Color verde para identificar pasajeros
    }

    public void SetWaitingState(bool waiting)
    {
        isWaiting = waiting;

        if (waiting)
        {
            // Si está esperando, activar la luz
            if (waitingLight != null)
            {
                waitingLight.enabled = true;
            }
        }
        else
        {
            // Si ya no está esperando, desactivar y destruir la luz
            if (waitingLight != null)
            {
                waitingLight.enabled = false;
                Destroy(waitingLight);
            }

            // Aquí podrías realizar otras acciones cuando deje de esperar, si es necesario
        }
    }

    private void Update()
    {
        // La luz se mantiene fija en la posición del pasajero
        if (waitingLight != null)
        {
            waitingLight.transform.position = transform.position; // Asegura que la luz siga la posición del pasajero
        }

        // No realizar ningún movimiento mientras isWaiting sea verdadero
        if (isWaiting)
        {
            return;
        }

        Debug.Log($"El pasajero está esperando con destino a: {destination}");
    }
}

