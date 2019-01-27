using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;
using System;
using Plugins.CloudCanards.Inspector;

public class FoodTools : MonoBehaviour
{
    public List<FoodTuple> starterList; //all foods and their states
    [ShowOnly]
    public Food foodProcessing;
    public Dictionary<Food,Food> myFoods; //same thing as starterList but it's a hashmap
    public PlayerStats player;
    [ShowOnly]
    public bool foodReady;
    public float timeLeft = 5; //5 seconds to cook each food
    private bool _timerStarted;

    public ProgressBar ProgressBar;

    public IEnumerator startTimer()
    {
        _timerStarted = true;

        var delta = 1f / timeLeft;
        var progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * delta;
            ProgressBar.Progress = Mathf.Clamp01(progress);
            yield return null;
        }

        ProgressBar.Progress = 0f;
        foodReady = true;
        _timerStarted = false;
    }

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
        if(foodProcessing == null){ //if tool is unused
            putFood();
        } else if(foodReady){ //after food is done cooking
            getFood();
        }
    }

    void putFood(){
        if(foodProcessing == null && player.holding != null && myFoods.ContainsKey(player.holding) && !_timerStarted){
            foodProcessing = player.holding;
            player.holding = null;
            StartCoroutine(startTimer());
        }
    }
    void getFood(){
        if(player.holding == null){
            player.holding = myFoods[foodProcessing];
            foodProcessing = null;
            foodReady = false;
        }
    }

    [Serializable]
    public class FoodTuple {
        public Food foodBefore;
        public Food foodAfter;
    }
}
