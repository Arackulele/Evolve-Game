using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VolFx;

public class ChangeBGColor : MonoBehaviour
{

    private Volume bgfilters;

    private AdjustmentsVol adjustments;
    // Start is called before the first frame update
    void Start()
    {

        bgfilters = GetComponent<Volume>();

        AdjustmentsVol temp;
        if (bgfilters.profile.TryGet<AdjustmentsVol>(out temp)) adjustments = temp;

        StartCoroutine(BGColorchanger());

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private IEnumerator BGColorchanger()
    {
        //this logic is kinda scuffed but produces random background colors well enough
        //also they dont interpolate all the way through which might be good because the hue will never change too much


        while (true) { 
        yield return new WaitForSeconds(Random.Range(9, 14));
        yield return new WaitForSeconds(Random.Range(9, 14));

            float oldHue = adjustments.m_Hue.value;

        float newHue = Random.Range(-0.99f, 0.99f);

        int t = 0;

        while (t < 10000 * 2)
            {
                print("Adjusting bg color");
                oldHue = adjustments.m_Hue.value;

                adjustments.m_Hue.Interp(oldHue, newHue, Time.deltaTime / 250);

                yield return new WaitForFixedUpdate();
                t++;
            }

        }

    }
}
