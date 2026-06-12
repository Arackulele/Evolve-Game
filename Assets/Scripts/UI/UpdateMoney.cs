using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMoney : MonoBehaviour
{
    private TMP_Text text;
    void Start()
    {
        text =  GetComponent<TMP_Text>(); 
    }

    void Update()
    {
        text.text = " " + StaticVars.money.ToString();
    }
}
