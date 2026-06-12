using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public abstract class FrontGun : MonoBehaviour
{
    public GameObject Bullet;

    public float cooldown = 100;

    CharacterController2D player;

    public AudioClip Audio;

    private List<AudioSource> Sound;

    private float maxcooldown;

    // Start is called before the first frame update
    void Start()
    {

        maxcooldown = cooldown;

        player = CharacterController2D.Instance;

        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.Find("SoundManager").GetComponents<AudioSource>());

    }

    void FixedUpdate()
    {
        if (cooldown < 1 && InputSystem.actions.FindAction("Attack").ReadValue<float>() > 0)
        {

            shoot();

            Sound.GetRandomItem().PlayOneShot(Audio);

            cooldown = maxcooldown;
        }
        else if (cooldown > -5 ) cooldown-= (player.baseCooldown);

    }

    public abstract void shoot();


}
