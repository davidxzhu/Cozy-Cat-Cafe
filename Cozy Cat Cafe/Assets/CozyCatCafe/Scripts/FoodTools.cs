using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

public class FoodTools : MonoBehaviour
{
    Food foodProcessing;
    Dictionary<Food,Food> myFoods;

    // Start is called before the first frame update
    void Start()
    {
        myFoods = new Dictionary<Food,Food>();
        foodProcessing = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        getResult();
    }

    Food getResult(){
        // if(myFoods[PlayerStats.foodCurrentlyHolding] == null || foodProcessing != null){

        // } else{
        //     foodProcessing = PlayerStats.foodCurrentlyHolding;

        //     PlayerStats.foodCurrentlyHolding = myFoods[PlayerStats.foodCurrentlyHolding];
        // }
        return null;
    }
}
