using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameLogic : MonoBehaviour
{
     void Awake() {
        AirConsole.instance.onMessage += OnMessage;    
     }

     void OnMessage(int fromDeviceID, JToken data){
         Debug.Log("message from " + fromDeviceID + ", data: " + data);
         if(data["action"] != null && data["action"].ToString().Equals("interact")){
             Camera.main.backgroundColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range(0f, 1f));
         }
     }

    void OnDestroy() {
        if(AirConsole.instance != null){
            AirConsole.instance.onMessage -= OnMessage;
        }         
     }
}
