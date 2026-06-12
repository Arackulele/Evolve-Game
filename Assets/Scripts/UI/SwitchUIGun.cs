using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUIGun : MonoBehaviour
{

    public CharacterController2D Ref;

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

            if (Ref.gun.GetChild(0).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[0];
            }

            else if (Ref.gun.GetChild(1).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[1];
            }

            else if (Ref.gun.GetChild(2).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[2];
            }

            else if (Ref.gun.GetChild(3).gameObject.activeSelf)
            {
                GetComponent<Image>().sprite = Guns[3];
            }
        }
    }
}
