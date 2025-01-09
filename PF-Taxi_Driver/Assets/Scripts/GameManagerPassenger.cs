using UnityEngine;

public class GameManagerPassenger : MonoBehaviour
{
    [SerializeField] private PassengerFactory passengerFactory; // Referencia al PassengerFactory
    private GameObject currentPassenger; // Referencia al pasajero actual

    private void Start()
    {
        Passenger.OnPassengerReachedDestination += HandlePassengerReachedDestination; // Suscribirse al evento
        CreateNewPassenger(); // Crear el primer pasajero
    }

    private void OnDestroy()
    {
        Passenger.OnPassengerReachedDestination -= HandlePassengerReachedDestination; // Desuscribirse del evento
    }

    private void HandlePassengerReachedDestination()
    {
        Debug.Log("El pasajero llegó al destino. Creando un nuevo pasajero...");
        CreateNewPassenger();
    }

    private void CreateNewPassenger()
    {
        if (currentPassenger == null || !currentPassenger.activeInHierarchy)
        {
            currentPassenger = passengerFactory.CreatePassenger();
        }
        else
        {
            Debug.Log("El pasajero actual aún no ha llegado al destino.");
        }
    }
}
