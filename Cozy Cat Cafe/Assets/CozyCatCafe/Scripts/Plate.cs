using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;
using System;

public class Plate : MonoBehaviour
{
    public PlayerStats player;
    public Dictionary<HashSet<Food>, Food> myMappings; 
    public HashSet<Food> onPlate;
    public Food dishToDisplay;
    public SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        myMappings = new Dictionary<HashSet<Food>, Food>(HashSet<Food>.CreateSetComparer());
        onPlate = new HashSet<Food>();
        dishToDisplay = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(player.holding != null) {
            putFood();
        } else if(!player.holdingPlate){
            player.holdingPlate = true;
            player.holdingDish = this;
        }
    }

    void putFood(){
        onPlate.Add(player.holding);
        player.holding = null;
        if(myMappings[onPlate] == null){

        } else{
            dishToDisplay = myMappings[onPlate];
        }
    }



    [Serializable]
    public class FoodMapping {
        public HashSet<Food> ingredients;
        public Food dishFromIngredients;
    }
}
