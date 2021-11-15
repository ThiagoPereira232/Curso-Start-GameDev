using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private int _totalWood;
    [SerializeField] private int _carrots;
    
    public float currentWater;
    private float waterLimit = 50;

    public int TotalWood { get => _totalWood; set => _totalWood = value; }
    public int Carrots { get => _carrots; set => _carrots = value; }

    public void WaterLimit(float water)
    {
        if(currentWater < waterLimit)
        {
            currentWater += water;
        }
    }
}
