using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health, healthmax = 10f;
    [SerializeField] HealthBar healthbar;

    private void Awake()
    {
        healthbar = GetComponentInChildren<HealthBar>();
        health = healthmax;
        healthbar.UpdateHealthBar(health, healthmax);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.UpdateHealthBar(health, healthmax);
        if (health <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
         Destroy(gameObject);
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
