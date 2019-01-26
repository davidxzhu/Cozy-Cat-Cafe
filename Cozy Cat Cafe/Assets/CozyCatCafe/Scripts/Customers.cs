using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public transform customer;
    public transform dish;


    private void Awake() {
        showDish();
    }

    void showDish(){

        Transform dishDisplay = Instantiate(dish, customer);
        dishDisplay.position.y = dishDisplay.position.y + 5f;     
    }

}
