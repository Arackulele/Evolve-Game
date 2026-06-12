using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Dump8BitDoControls : MonoBehaviour
{
    void Update()
    {
        foreach (var device in InputSystem.devices)
        {
            if (!device.displayName.ToLower().Contains("8bit"))
                continue;

            foreach (var control in device.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    Debug.Log($"BUTTON: {control.path}");
                }

                if (control is AxisControl axis)
                {
                    float value = axis.ReadValue();
                    if (Mathf.Abs(value) > 0.75f)
                    {
                        Debug.Log($"AXIS: {control.path} = {value}");
                    }
                }
            }
        }
    }
}