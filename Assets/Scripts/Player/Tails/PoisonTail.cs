using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoisonTail : TailBehaviour
{

    private ParticleSystem poison;

    public GameObject hitbox;

    public override void Activate()
    {

        poison = transform.GetChild(0).GetComponent<ParticleSystem>();

        StartCoroutine(ExcretePoison());

    }


    IEnumerator ExcretePoison()
    {

        poison.enableEmission = true;

        //1.5 seconds active total 
        int i = 0;
        while (i < 8)
            {
            GameObject h = GameObject.Instantiate(hitbox);

            h.transform.position = transform.position;
            h.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(0.25f);
            i++;
            }

        poison.enableEmission = false;

    }

}
