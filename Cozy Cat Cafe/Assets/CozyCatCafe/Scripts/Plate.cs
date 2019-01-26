using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;
using System;

public class Plate : MonoBehaviour
{
    public PlayerStats player;
    public HashSet<Food> onPlate; //all the ingredients on the plate currently
    public List<FoodStates> myMappings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(player.holding != null) {
            putFood();
        } else {
            getFood();
        }
    }

    void putFood(){
        
    }
    void getFood(){
        
    }

    [Serializable]
    public class FoodStates {
        public HashSet<Food> ingredients;
        public Food dishFromIngredients;
    }
}
