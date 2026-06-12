using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TailSpike : MonoBehaviour
{

    public AudioClip Audio;

    GameObject Cam;

    private List<AudioSource> Sound;

    public float damage;

    public Vector3 originalPos;


    void Start()
    {
        //Change how cam is got
        Cam = GameObject.Find("Main Camera");

        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.Find("SoundManager").GetComponents<AudioSource>());
    }

    private void FixedUpdate()
    {

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.004f, transform.localPosition.z);

    }

    private void OnEnable()
    {
        transform.localPosition = originalPos;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.GetComponent<EnemyBehavior>() != null)
        {
            col.GetComponent<EnemyBehavior>().dealDamage(damage);

            //Sound.GetRandomItem().PlayOneShot(Audio);

            Cam.GetComponent<ScreenShake>().shakeDuration += 0.1f;
        }
    }


}
