using UnityEngine;
using TMPro;

public class VehicleSpeedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText; // Referencia al texto en el Canvas
    [SerializeField] private CarController carController; // Referencia al CarController

    private void Update()
    {
        if (carController != null)
        {
            // Calcula la velocidad en km/h a partir del Rigidbody del CarController
            float speed = carController.GetComponent<Rigidbody>().velocity.magnitude * 3.6f; // Convertir m/s a km/h
            // Actualiza el texto en el Canvas
            speedText.text = $"Velocidad: {Mathf.RoundToInt(speed)} km/h";
        }
        else
        {
            Debug.LogWarning("CarController no asignado en VehicleSpeedUI.");
        }
    }
}


