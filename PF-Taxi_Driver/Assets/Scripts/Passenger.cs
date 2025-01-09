using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Vector3 destination; // Destino del pasajero

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
        Debug.Log($"Destino asignado al pasajero: {destination}");
    }
}
