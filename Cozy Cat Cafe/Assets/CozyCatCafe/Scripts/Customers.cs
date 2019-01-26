using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public Transform customer;
    public Transform dish;


    private void Awake() {
        showDish();
    }

    void showDish(){

        Transform dishDisplay = Instantiate(dish, customer);
        dishDisplay.position = new Vector3(dishDisplay.position.x, dishDisplay.position.y + 5f, dishDisplay.position.z);     
    }

}
