using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FearManager : MonoBehaviour
{
    public static FearManager instance { get; private set; }
    public Image fillingImage;
    public SpotlightController spotlightController;
    public float maxFear = 100f;
    public float currentFear = 10f;
    private void Start()
    {
        currentFear = 10f;
        UpdateFearBar();
    }

    public static FearManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {     
        //if (PriestVisibilityCheck.instance.priestIsInCameraView )
        //    ModifyHealth(-10f * Time.deltaTime);

        if (Flashlight.instance.priestIsInFlashlight && currentFear <= maxFear)
            ModifyFear(25f * Time.deltaTime);
        else if (spotlightController.playerInLightRay && currentFear <= maxFear)
            ModifyFear(25f * Time.deltaTime);
        else if (currentFear <= maxFear && currentFear >= 10f)
            ModifyFear(-5f * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.P) && currentFear <= maxFear)
        {
            ModifyFear(100f * Time.deltaTime);
        }       
        if (Input.GetKeyDown(KeyCode.L))
        {
            ModifyFear(-100f * Time.deltaTime);
        }
    }
    private void UpdateFearBar()
    {    
        float fillAmount = currentFear / maxFear;
        fillingImage.fillAmount = fillAmount;
    }
    public void ModifyFear(float amount)
    {
        currentFear = Mathf.Clamp(currentFear + amount, 0, maxFear);
        UpdateFearBar();
        
        if (currentFear <= 0)
        {
            
        }
    }
}

