using System;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public Vector3 destination; // Destino del pasajero
    public static event Action OnPassengerReachedDestination; // Evento que notifica al llegar al destino

    public bool isWaiting = true; // Indica si el pasajero est� esperando
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
        waitingLight.type = LightType.Spot; // Tipo de luz punto para marcar solo la posici�n
        waitingLight.range = 10; // Alcance corto para indicar la posici�n exacta
        waitingLight.intensity = 6; // Intensidad adecuada para hacerlo visible
        waitingLight.color = Color.blue; // Color verde para identificar pasajeros
    }

    public void SetWaitingState(bool waiting)
    {
        isWaiting = waiting;

        if (waiting)
        {
            // Si est� esperando, activar la luz
            if (waitingLight != null)
            {
                waitingLight.enabled = true;
            }
        }
        else
        {
            // Si ya no est� esperando, desactivar y destruir la luz
            if (waitingLight != null)
            {
                waitingLight.enabled = false;
                Destroy(waitingLight);
            }

            // Aqu� podr�as realizar otras acciones cuando deje de esperar, si es necesario
        }
    }

    private void Update()
    {
        // La luz se mantiene fija en la posici�n del pasajero
        if (waitingLight != null)
        {
            waitingLight.transform.position = transform.position; // Asegura que la luz siga la posici�n del pasajero
        }

        // No realizar ning�n movimiento mientras isWaiting sea verdadero
        if (isWaiting)
        {
            return;
        }

        Debug.Log($"El pasajero est� esperando con destino a: {destination}");
    }
}

