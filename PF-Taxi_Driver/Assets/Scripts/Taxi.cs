using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : MonoBehaviour
{
    [SerializeField] float health, healthmax = 10f;
    [SerializeField] HealthBar healthbar;

    private bool hasPassenger; // Variable para indicar si el taxi tiene un pasajero

    // Evento para notificar cuando se deja al pasajero
    public Action OnPassengerDroppedOff;

    private void Awake()
    {
        healthbar = GetComponentInChildren<HealthBar>();
        health = healthmax;
        healthbar.UpdateHealthBar(health, healthmax);
        hasPassenger = false; // Inicialmente, el taxi no tiene pasajero
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            healthbar.UpdateHealthBar(health, healthmax);
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Min(health, healthmax); // Asegurarse de que no exceda el máximo
        healthbar.UpdateHealthBar(health, healthmax);
        Debug.Log($"Taxi curado. Vida actual: {health}");
    }

    public void Die()
    {
        Debug.Log("El taxi ha muerto. Fin del juego.");
        EndGame();
        Destroy(gameObject);
    }

    private void EndGame()
    {
        Time.timeScale = 0; // Pausar el tiempo del juego
        Debug.Log("Game Over. El juego se ha pausado.");
    }

    public void PickUpPassenger()
    {
        hasPassenger = true;
        Debug.Log("Pasajero recogido.");
    }

    public void DropOffPassenger()
    {
        hasPassenger = false;
        Debug.Log("Pasajero dejado.");

        // Invocar el evento de pasajero dejado
        OnPassengerDroppedOff?.Invoke();
    }

    public bool HasPassenger()
    {
        return hasPassenger;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Identificar el objeto con el que colisiona
        if (collision.gameObject.CompareTag("PoliceCar")) // Colisión con coche de policía
        {
            TakeDamage(3); // Reduce 3 puntos de vida
            Debug.Log("Colisión con policía. Vida restante: " + health);
        }
        else if (collision.gameObject.CompareTag("Obstacle")) // Colisión con obstáculo genérico
        {
            TakeDamage(1); // Reduce 1 punto de vida
            Debug.Log("Colisión con obstáculo. Vida restante: " + health);
        }
    }
}



