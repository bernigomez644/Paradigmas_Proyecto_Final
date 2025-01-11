using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update

    public void UpdateHealthBar(float currentvalue,float maxvalue)
    {
        slider.value = currentvalue/maxvalue;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}