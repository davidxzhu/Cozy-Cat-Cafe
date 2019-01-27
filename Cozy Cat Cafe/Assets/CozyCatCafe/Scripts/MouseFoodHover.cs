using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

public class MouseFoodHover : MonoBehaviour
{
    public Transform foodSource;
    public Sprite foodDisplay;
    public DialogueBubble bubble;

    private void OnMouseEnter() {

        bubble.ChangeSprite(foodDisplay); 
    }

    private void OnMouseExit() {

        bubble.ChangeSprite(null);
    }
}
