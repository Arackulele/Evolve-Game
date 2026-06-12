using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoneyBehaviour : MonoBehaviour
{
    public AudioClip Audio;

    private List<AudioSource> Sound;

    void Start()
    {


        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.Find("SoundManager").GetComponents<AudioSource>());

    }

    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.GetComponent<CharacterController2D>() != null)
        {
            StaticVars.money++;

            AudioSource TP = Sound.GetRandomItem();
            TP.pitch = Random.Range(0.8f, 1.2f);

            TP.PlayOneShot(Audio);

            TP.pitch = 1;

            Destroy(gameObject);
        }
    }


}
