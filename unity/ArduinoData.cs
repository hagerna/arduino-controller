using UnityEngine;


[System.Serializable]
public class ArduinoData
{
    public ArduinoInputs arduino_inputs;
}

[System.Serializable]
public class ArduinoInputs
{
    public int leftPrimaryButton;
    public int rightPrimaryButton;
    public int leftSecondaryButton;
    public int rightSecondaryButton;
}