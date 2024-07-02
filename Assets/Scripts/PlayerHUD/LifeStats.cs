using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class LifeStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image[] lifeCrystals;

    [Range(0f, 5f)]
    [SerializeField] int health;
    private int prevHealth;

    
    private void Start() {
        prevHealth = health;
        updateHealth(5);
    }

    public void Update() 
    {
        if(health != prevHealth) {
            updateHealth(health);
            prevHealth = health;
        }
    }

    public void updateHealth(int health) 
    {
       for(int i = 0; i < 5; i++) 
       {
            if(i < health)
            {
                lifeCrystals[i].GetComponent<lifeCrystals>().changeCrystal(true);
            }
            else
            {
                lifeCrystals[i].GetComponent<lifeCrystals>().changeCrystal(false);

            }
       } 
    }
}
