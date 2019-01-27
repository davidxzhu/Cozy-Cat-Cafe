using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CozyCatCafe.Scripts;

public class PlayerInfoUI : MonoBehaviour
{
    public PlayerStats PlayerInfo;
    public Image CurrentFoodDisplay;
    public Text CurrentMoneyDisplay;
    private Food CurrentHolding;
    private int CurrentMoney;

    // Update is called once per frame
    private void Awake() {
        CurrentMoney = PlayerInfo.Money;
        CurrentMoneyDisplay.text = CurrentMoney.ToString();
    }
    void Update()
    {
        if(CurrentMoney != PlayerInfo.Money){
            CurrentMoney = PlayerInfo.Money;
            CurrentMoneyDisplay.text = CurrentMoney.ToString();
        }


        if(CurrentHolding != PlayerInfo.holding){
            
            if(PlayerInfo.holding != null){
                CurrentFoodDisplay.sprite = PlayerInfo.holding.Sprite;
            }
            else{
                CurrentFoodDisplay.sprite = null;
            }
            CurrentHolding = PlayerInfo.holding;
        }

        
    }
}
