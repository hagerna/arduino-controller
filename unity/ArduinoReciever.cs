using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;
using System;

public class ArduinoReciever : MonoBehaviour
{
    [SerializeField]
    private string serverURI = "ws://localhost:8080";
    private WebSocket ws;

    public static UnityEvent<ArduinoData> onDataRecieved = new UnityEvent<ArduinoData>();

    public ArduinoData ARDUINO_DEFAULT = JsonUtility.FromJson<ArduinoData>("{\"arduino_inputs\": {\"leftPrimaryButton\":1,\"rightPrimaryButton\":0,\"leftSecondaryButton\":0,\"rightSecondaryButton\":0}}");

    void Start()
    {
        //onDataRecieved.Invoke(ARDUINO_DEFAULT);
        // Replace with your local server URI
        ws = new WebSocket(serverURI);

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Opened");
        };

        ws.OnMessage += (sender, e) =>
        {
            ArduinoData data = JsonUtility.FromJson<ArduinoData>(e.Data);
            if (onDataRecieved != null)
            {
                try
                {
                    onDataRecieved.Invoke(data);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception thrown by onDataRecieved handler: " + ex);
                }
            }
            else
            {
                Debug.LogWarning("onDataRecieved was null — nothing to invoke.");
            }
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket Error: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Closed");
        };

        ws.Connect();
    }

    void OnApplicationQuit()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }
}


