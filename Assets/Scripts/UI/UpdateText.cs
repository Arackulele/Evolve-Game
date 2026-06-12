using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    private TMP_Text text;
    void Start()
    {
        text =  GetComponent<TMP_Text>(); 
    }

    void Update()
    {
        text.text = "Score " + StaticVars.highscore.ToString();
    }
}
