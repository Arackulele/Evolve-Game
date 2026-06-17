using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class ActivateAbility : MonoBehaviour
{

    public GameObject icon;

    public Sprite iconSprite;

    public int cooldown;

    public CharacterController2D player;

    protected int timer;

    protected bool usable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (timer > 0) {
            timer--;
            //change cooldown to dynamically update image based on timer
            icon.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
        }
        else {
            icon.GetComponent<Image>().color = new Color(1, 1, 1);
            usable = true;
        }


        
    }

    public abstract void Activate();

    public abstract void ActivateConditions();

    private void OnEnable()
    {
        timer = cooldown;
        icon.GetComponent<Image>().sprite = iconSprite;
        //for poison tail
        if (transform.GetChild(0).GetComponent<ParticleSystem>() != null) transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;

    }
}