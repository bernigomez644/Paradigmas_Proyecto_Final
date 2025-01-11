using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public bool isPursuing = false; // Indica si el coche de policía está persiguiendo
    public event Action<string> OnPursuing;

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
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            agent.destination = player.position;

            //Ajusta la velocidad según la distancia
            if (distanceToPlayer < 10f) // A menos de 10 unidades, reduce la velocidad
            {
              agent.speed = 30f; // Velocidad reducida
            }
            else
            {
                agent.speed = 40f; // Velocidad normal
            }

            agent.destination = player.position; // Persigue al jugador
        }
    }

    private void StartPursuit()
    {
        isPursuing = true; // Activa la persecución
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        CarController.OnExceedSpeedLimit -= StartPursuit;
    }
}
