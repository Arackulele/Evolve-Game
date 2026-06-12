using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUITail : MonoBehaviour
{

    public Transform Tail;

    public List<Sprite> Guns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController2D.Instance)
        {

            if (Tail.GetChild(0).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[0];
            }

            else if (Tail.GetChild(1).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[1];
            }

            else if (Tail.GetChild(2).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[2];
            }

            else GetComponent<Image>().sprite = Guns[3];

        }
    }
}
