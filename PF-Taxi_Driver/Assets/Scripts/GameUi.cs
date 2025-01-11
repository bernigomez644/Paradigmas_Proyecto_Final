using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private GameManagerSinBusqueda m_GameManagerSinBusqueda;

    private void OnEnable()
    {
        // Suscribirse al evento
        if (m_GameManagerSinBusqueda != null)
        {
            m_GameManagerSinBusqueda.Onchanged += Imprimir;
        }
    }

    private void OnDisable()
    {
        // Desuscribirse del evento
        if (m_GameManagerSinBusqueda != null)
        {
            m_GameManagerSinBusqueda.Onchanged -= Imprimir;
        }
    }

    private void Imprimir(string accion)
    {
        // Verificar si la acción ya está en la lista
        if (accion == "Pasajero entregado, +5 de vida.")
        {
            StartCoroutine(HideMessageAfterDelay(3f)); // Cambia 3f por los segundos que desees
        }

        // Mostrar el mensaje en el TextMeshPro
        if (m_TextMeshPro != null)
        {
            m_TextMeshPro.text = accion;
        }

        // Si la acción es "Pasajero dejado en su destino.", iniciar la corrutina para ocultarla después
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Verificar que m_TextMeshPro no sea nulo antes de modificarlo
        if (m_TextMeshPro != null)
        {
            m_TextMeshPro.text = ""; // Limpiar el texto después del tiempo
        }
    }

}
