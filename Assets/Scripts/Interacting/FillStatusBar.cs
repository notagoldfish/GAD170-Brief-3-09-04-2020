using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FillStatusBar : MonoBehaviour

{

    public character maxHealth;
    public character currentHealth;
    public character playerHealth;
    public Image fillImage;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        if(fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }

              
       else if (fillValue > slider.maxValue / 3)
       {
          fillImage.color = Color.red;
       }
        slider.value = fillValue;
    }

}
