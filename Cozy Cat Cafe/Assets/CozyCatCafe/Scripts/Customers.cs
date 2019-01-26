using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CozyCatCafe.Scripts;

public class Customers : MonoBehaviour
{
    private State state;

    public Transform customer;
    public Transform dish;
    public Transform seat;
    public float customerMoveSpeed;

    [Header("Food")]
    public DialogueBubble bubble;

    public Food orderDish;
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
        if (state == State.WalkingIn) {
            if (Vector3.Distance(customer.position, seat.position) > 0.1)
                goToSeat();
            else {
                state = State.Ordering;
                createDish();
            }
        } 
        
        else if (state == State.Ordering) {
            if (!gotFood)
                return;
            else {
                state = State.Eating;
                bubble.ChangeSprite(null);
            }
        } 
        
        else if (state == State.Eating) {
            state = State.WalkingOut;
        } 
        
        else {
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
        bubble.ChangeSprite(orderDish.Sprite);
    }

    void leave(){
        customer.position = customer.position + moveStep;
        currentOpacity -= 1/fadeStep;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,currentOpacity);

        if(currentOpacity < 0){
            Destroy(gameObject);
        }
    }

    public enum State {
        WalkingIn,
        Ordering,
        Eating,
        WalkingOut
    }


}
