using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

public class Customers : MonoBehaviour
{
    public Transform customer;
    public Transform dish;
    public Transform seat;
    public float customerMoveSpeed;

    [Header("Food")]
    public Transform bubble;
    public SpriteRenderer foodSprite;

    public Food orderDish;


    private Transform dishDisplay;
    private bool isSitting = false;
    private bool dishCreated = false;
    public bool gotFood;
    private Vector3 moveStep;
    public float fadeStep;
    private float currentOpacity = 0;
    //private float currentTime = 0f;
    private void Awake() {
        //createDish();
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,currentOpacity);

        //Used for testing
        moveStep = (customer.position - seat.position) / customerMoveSpeed;
        //fadeStep = 1 / Vector3.Distance(customer.position, seat.position);

    }

    private void Update() {

        if(!isSitting && Vector3.Distance(customer.position, seat.position) > 0.1){
            goToSeat();
        }
        else{
            isSitting = true;
            if(!dishCreated){
                createDish();
            }
            dishCreated = true;
        }

        if(gotFood && isSitting){
            Destroy(dishDisplay);
            leave();
        }
    }

    // Used for Spawner
    void setSeat(Transform seat){
        this.seat = seat;
        moveStep = (seat.position - customer.position) / customerMoveSpeed;
        fadeStep = 1 / customerMoveSpeed;//Vector3.Distance(customer.position, seat.position);
    }

    void goToSeat(){
        customer.position = customer.position - moveStep;
        currentOpacity += 1/fadeStep;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,currentOpacity);
    }

    void createDish(){

        dishDisplay = Instantiate(dish, customer);
        dishDisplay.position = new Vector3(customer.position.x, customer.position.y + 1, customer.position.z);     
    }

    void leave(){
        customer.position = customer.position + moveStep;
        currentOpacity -= 1/fadeStep;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,currentOpacity);

        if(currentOpacity < 0){
            Destroy(gameObject);
        }
    }



}
