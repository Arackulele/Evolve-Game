using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUISideGun : MonoBehaviour
{

    public Transform SideGunSlot;

    public int dir = 90;

    public List<Sprite> Guns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController2D.Instance) { 

            if (SideGunSlot.GetChild(0).gameObject.activeSelf)
            {
            GetComponent<Image>().sprite = Guns[0];
            this.transform.localRotation = SideGunSlot.GetChild(0).localRotation * Quaternion.Euler(0, 0, dir);
            }

            else if (SideGunSlot.GetChild(1).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[1];
                this.transform.localRotation = SideGunSlot.GetChild(1).localRotation * Quaternion.Euler(0, 0, dir);
            }

            else if (SideGunSlot.GetChild(2).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[2];
                this.transform.localRotation = SideGunSlot.GetChild(2).localRotation * Quaternion.Euler(0, 0, dir);
            }

            else GetComponent<Image>().sprite = Guns[3];


        }

    }
}
