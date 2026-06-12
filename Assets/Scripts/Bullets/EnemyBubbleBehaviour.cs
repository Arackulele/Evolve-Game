using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBubbleBehavior : BaseBulletBehavior
{
    public bool accelerate = true;

    private Vector3 origscale;

    // Start is called before the first frame update
    void Start()
    {

        origscale = transform.localScale;

        StartCoroutine(DestroyAfterTime());

        //Change how cam is got
        Cam = Camera.main.gameObject;

        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.Find("SoundManager").GetComponents<AudioSource>());

        update = true;
    }


    public override void UpdateBehaviour()
    {

        if (transform.localScale.x < origscale.x * 8) transform.localScale = new Vector3(transform.localScale.x + 0.007f, transform.localScale.y + 0.007f, transform.localScale.z + 0.007f);
        if (speed < 4 && accelerate) speed += speed * 0.004f;
        else if (!accelerate) speed -= speed * 0.004f;
        transform.position += transform.right * speed * Time.deltaTime;

    }


    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(ScaleDown());

        transform.GetChild(1).gameObject.SetActive(true);

        speed = 0;

        if (!collide && Sound.GetRandomItem() != null) Sound.GetRandomItem().PlayOneShot(Audio);

        yield return new WaitForSeconds(0.2f);

        transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().emissionRate = 0;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    private IEnumerator ScaleDown()
    {

    while(true) 
        {

        yield return new WaitForEndOfFrame();

        if (transform.localScale.x > 0) transform.localScale = new Vector3(transform.localScale.x - 0.04f, transform.localScale.y - 0.04f, transform.localScale.z - 0.04f); ;
        
        }


    }
}
