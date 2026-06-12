using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpikeTail : TailBehaviour
{

    private GameObject spikes;

    public override void Activate()
    {

        spikes = transform.GetChild(0).gameObject;

        StartCoroutine(ExtendSpikes());

    }

    IEnumerator ExtendSpikes()
    {

    spikes.SetActive(true);

    yield return new WaitForSeconds(0.6f);

    spikes.SetActive(false);

    }

}
