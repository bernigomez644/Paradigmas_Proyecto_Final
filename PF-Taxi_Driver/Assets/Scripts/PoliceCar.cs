using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private bool isPursuing = false; // Indica si el coche de polic�a est� persiguiendo

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<CarController>().transform;

        // Suscribirse al evento de exceso de velocidad
        CarController.OnExceedSpeedLimit += StartPursuit;
    }

    private void Update()
    {
        if (isPursuing)
        {
            agent.destination = player.position; // Persigue al jugador solo si est� activo
        }
    }

    private void StartPursuit()
    {
        isPursuing = true; // Activa la persecuci�n
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        CarController.OnExceedSpeedLimit -= StartPursuit;
    }
}
