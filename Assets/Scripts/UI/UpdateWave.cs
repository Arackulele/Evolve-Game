using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWave : MonoBehaviour
{
    private TMP_Text text;
    void Start()
    {
        text =  GetComponent<TMP_Text>(); 
    }

    void Update()
    {
        text.text = "Wave " + StaticVars.wave.ToString();
    }
}
