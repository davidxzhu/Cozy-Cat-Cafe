using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

public class FoodTools : MonoBehaviour
{
    Food foodProcessing;
    Dictionary<Food,Food> myFoods;
    PlayerStats player;
    bool foodReady;

    // Start is called before the first frame update
    void Start()
    {
        myFoods = new Dictionary<Food,Food>();
        foodProcessing = null;
        foodReady = false;
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
}
