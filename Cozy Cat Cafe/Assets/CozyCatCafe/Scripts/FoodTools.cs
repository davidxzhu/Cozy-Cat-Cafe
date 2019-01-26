using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;
using System;

public class FoodTools : MonoBehaviour
{
    public List<FoodTuple> starterList;
    public Food foodProcessing;
    public Dictionary<Food,Food> myFoods;
    public PlayerStats player;
    public bool foodReady;

    // Start is called before the first frame update
    void Start()
    {
        myFoods = new Dictionary<Food,Food>();
        foreach(FoodTuple item in starterList){
            myFoods[item.foodBefore] = item.foodAfter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(foodProcessing == null){
            putFood();
        } else{
            if(foodReady){
                getFood();
                foodReady = false;
            }
        }
    }

    void putFood(){
        if(myFoods[player.foodCurrentlyHolding] != null){
            foodProcessing = player.foodCurrentlyHolding;
            player.foodCurrentlyHolding = null;
        }
    }
    void getFood(){
        if(player.foodCurrentlyHolding == null){
            player.foodCurrentlyHolding = myFoods[foodProcessing];
            foodProcessing = null;
        }
    }

    [Serializable]
    public class FoodTuple {
        public Food foodBefore;
        public Food foodAfter;
    }
}
