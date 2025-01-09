using System;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Vector3 destination; // Destino del pasajero
    public static event Action OnPassengerReachedDestination; // Evento que notifica al llegar al destino

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
        Debug.Log($"Destino asignado al pasajero: {destination}");
    }

    private void Update()
    {
        // Simula el movimiento hacia el destino (puedes reemplazarlo con lógica real)
        transform.position = Vector3.MoveTowards(transform.position, destination, 5f * Time.deltaTime);

        // Comprueba si el pasajero ha llegado a su destino
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            Debug.Log("Pasajero ha llegado a su destino.");
            OnPassengerReachedDestination?.Invoke(); // Notifica que ha llegado
            gameObject.SetActive(false); // Desactiva al pasajero
        }
    }
}
