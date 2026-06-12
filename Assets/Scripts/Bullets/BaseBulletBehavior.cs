using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class BaseBulletBehavior : MonoBehaviour
{
    public bool update = false;

    public float basedamage;

    public float speed = 10f;

    public float lifetime = 2;

    public AudioClip Audio;

    public bool piercing = false;

    public GameObject Cam;

    public float damagemodifier = 0;

    public bool collide = true;

    public List<AudioSource> Sound;

    public bool enemy = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());

        //Change how cam is got
        Cam = Camera.main.gameObject;

        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.FindWithTag("Sound").GetComponents<AudioSource>());

        update= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {

            UpdateBehaviour();
            transform.position += transform.right * speed * Time.deltaTime;
        }

    }

    public abstract void UpdateBehaviour();

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (!enemy) { 
            if (col.GetComponent<EnemyBehavior>() != null && collide)
            {

                if (!update) yield return new WaitForSeconds(0.02f);
                if (col != null) col.GetComponent<EnemyBehavior>().dealDamage(basedamage + damagemodifier);

                Cam.GetComponent<ScreenShake>().shakeDuration += (basedamage + damagemodifier) * 0.05f;

                if (Sound.GetRandomItem() != null) Sound.GetRandomItem().PlayOneShot(Audio);

                if (!piercing) Destroy(gameObject);
            }

        }
        else if (col.GetComponent<CharacterController2D>() != null && collide)
        {

            if (!update) yield return new WaitForSeconds(0.02f);
            StartCoroutine(CharacterController2D.Instance.TakeDamage(basedamage));


            if (Sound.GetRandomItem() != null) Sound.GetRandomItem().PlayOneShot(Audio);

            if (!piercing) Destroy(gameObject);

        }

        //ToDO: Fix Audio


        yield break;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        if (!collide && Sound.GetRandomItem() != null) Sound.GetRandomItem().PlayOneShot(Audio);

        Destroy(gameObject);
    }

}
