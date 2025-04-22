using UnityEngine;

public class ArduinoObject : MonoBehaviour
{

    Rigidbody rb;
    Transform ts;
    private ArduinoInputs inputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ts = transform;
        ArduinoReciever.onDataRecieved.AddListener(HandleArduinoInputs);
    }

    void HandleArduinoInputs(ArduinoData data)
    {
        inputs = data.arduino_inputs;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs != null)
        {
            if (inputs.leftPrimaryButton != 0)
            {
                transform.RotateAround(Vector3.zero, Vector3.up, -30f * Time.deltaTime);
            }
            if (inputs.rightPrimaryButton != 0)
            {
                transform.RotateAround(Vector3.zero, Vector3.up, 30f * Time.deltaTime);
            }
        }
    }
}
