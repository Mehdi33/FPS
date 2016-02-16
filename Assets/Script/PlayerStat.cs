using UnityEngine;
using System.Collections;

public class PlayerStat : MonoBehaviour {


    public int grenades = 2;
    public float timebeforehealth = 0;
    public int healthbase = 100;
    public int healthmax = 100;

    public GameObject bloodUI;
    void Start()
    {
        InvokeRepeating("Regen", 0, 1);
        InvokeRepeating("reduceTimer", 0, 1);
    }
    
    // Use this for initialization
    void ApplyDammage (int TheDammage) {
        healthbase -= TheDammage;
        timebeforehealth += 5;
        if(healthbase <= 0)
        {
            Dead();
        }
	}
	
	// Update is called once per frame
	void Dead () {
        Debug.Log("Player died");
	}
    void Update()
    {
        
        

        if (healthbase >=1 && healthbase < 30)
        {
            bloodUI.GetComponent<CanvasGroup>().alpha = 1;
        }

        if (healthbase >= 30 && healthbase < 60)
        {
            bloodUI.GetComponent<CanvasGroup>().alpha = 0.6F;
        }
        if (healthbase >= 60 && healthbase < 80)
        {
            bloodUI.GetComponent<CanvasGroup>().alpha = 0.3F;
        }
        if (healthbase >= 80 && healthbase < 100)
        {
            bloodUI.GetComponent<CanvasGroup>().alpha = 0;
        }

        if(healthbase > 100)
        {
            healthbase = 100;
        }

        if(timebeforehealth >= 5)
        {
            timebeforehealth = 5;
        }
        if (timebeforehealth <= 0)
        {
            timebeforehealth =0;
        }
    }

    void reduceTimer()
    {
        timebeforehealth -= 1;
    }

    void Regen()
    {
        if(timebeforehealth == 0)
        {
            healthbase += 20;
        }
    }
}
