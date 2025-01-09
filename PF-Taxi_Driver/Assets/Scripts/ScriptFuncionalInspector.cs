using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AddRoadTileScript : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("Añadir RoadTile a Hijos")]
    private void AddRoadTileToChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<RoadTile>() == null) // Comprueba si no tiene el script
            {
                child.gameObject.AddComponent<RoadTile>();
                Debug.Log($"Añadido RoadTile a {child.name}");
            }
        }

        Debug.Log("Todos los hijos tienen el script RoadTile.");
    }
#endif
}

