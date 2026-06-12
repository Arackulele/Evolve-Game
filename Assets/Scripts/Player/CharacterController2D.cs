using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class CharacterController2D : MonoBehaviour
{

    public static CharacterController2D Instance => FindObjectOfType<CharacterController2D>();


    public float speed = 9;

    public float Acceleration = 50;

    public float Decelaration = 50;

    public float GravityMod = -3.4f;

    public GameObject particleSystem;
    public GameObject particleSystem2;

    private BoxCollider2D boxCollider;

    public Vector2 velocity;

    public Vector2 passivevelocity;

    public Transform gun;
    public Transform sidegun1;
    public Transform sidegun2;
    public Transform tail;

    public Scrollbar HPBar;

    public GameObject shipsprite;

    private float hp = 10;
    public float maxhp = 10;
    public float healrate = 0.0005f;

    public List<Upgrade> Upgrades = new List<Upgrade>();

    public float baseCooldown = 1;
    public float baseDamage = 0;
    public float baseSideDamage = 0;
    public float basesideCooldown = 1;

    public GameObject Cam;

    public GameObject ExitScreen;

    private ParticleSystem hitparticles;


    public AudioClip HitSound;

    private List<AudioSource> Sound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        hitparticles = transform.GetChild(1).GetComponent<ParticleSystem>();

        hitparticles.enableEmission = false;


        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.Find("SoundManager").GetComponents<AudioSource>());

    }

    private void FixedUpdate()
    {

        MoveCharacterBasedOnInput();
        TeleportPlayerAtScreenBorder();

        StaticVars.health = hp;

        HPBar.size = hp / maxhp;

        if (hp < maxhp) hp += healrate;

    }

    private void MoveCharacterBasedOnInput()
    {

        float moveInput = InputSystem.actions.FindAction("Move").ReadValue<Vector2>().x;
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, Acceleration * Time.deltaTime);
            
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, Decelaration * Time.deltaTime);
        }

        float moveInputup = InputSystem.actions.FindAction("Move").ReadValue<Vector2>().y;
        if (moveInputup != 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, (speed * moveInputup) + GravityMod, Acceleration * Time.deltaTime);
        }
        else
        {
            velocity.y = Mathf.MoveTowards(velocity.y, GravityMod, Decelaration * Time.deltaTime);
        }

        float ang = Mathf.Atan2(moveInput, moveInputup) * Mathf.Rad2Deg;
        if (moveInput > 0.1 || moveInputup > 0.1 || moveInput < -0.1 || moveInputup < -0.1) shipsprite.transform.rotation = Quaternion.RotateTowards(shipsprite.transform.rotation, Quaternion.AngleAxis(-ang, Vector3.forward), 400f * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);

        transform.Translate(passivevelocity);



        Vector2 moveDirection;

        moveDirection = InputSystem.actions.FindAction("Look").ReadValue<Vector2>();
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            Quaternion n = Quaternion.RotateTowards(gun.rotation * Quaternion.Euler(0, 0, 90), Quaternion.AngleAxis(angle, Vector3.forward), 300f * Time.deltaTime);
            gun.rotation = n * Quaternion.Euler(0, 0, -90);

        }

    }

    private void TeleportPlayerAtScreenBorder()
    {
        if (!takingdamage) { 

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0f)
        {
            particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
            particleSystem2.GetComponent<ParticleSystem>().enableEmission = false;
            pos = new Vector3(1.0f, pos.y, pos.z);
            //particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(ReenableParticles());
        }
        else if (pos.x >= 1.0f)
        {
            particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
            particleSystem2.GetComponent<ParticleSystem>().enableEmission = false;
            pos = new Vector3(0.0f, pos.y, pos.z);
            //particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(ReenableParticles());
        }
        if (pos.y < 0.0f)
        {
            particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
            particleSystem2.GetComponent<ParticleSystem>().enableEmission = false;
            pos = new Vector3(pos.x, 1.0f, pos.z);
            //particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(ReenableParticles());
        }
        else if (pos.y >= 1.0f)
        {
            particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
            particleSystem2.GetComponent<ParticleSystem>().enableEmission = false;
            pos = new Vector3(pos.x, 0.0f, pos.z);
            //particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(ReenableParticles());
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        }
    }

    private IEnumerator ReenableParticles()
    {
        yield return new WaitForSeconds(0.07f);
        particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
        particleSystem2.GetComponent<ParticleSystem>().enableEmission = true;
    }

    private bool takingdamage = false;

    public IEnumerator TakeDamage(float damage) 
    {

        Sound.GetRandomItem().PlayOneShot(HitSound);

        if (takingdamage == false && ( damage >= maxhp * 0.3f || (hp < maxhp*0.3f && damage > hp/2) )) {

            takingdamage = true;

            Time.timeScale = 0.2f;

            hp -= damage;
            hitparticles.enableEmission = true;

            float camsize = Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize;

            while (Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize > 3.8f)
            {
                Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize, camsize * 0.4f, 0.07f);
                Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, transform.position, 0.2f);
                yield return new WaitForEndOfFrame();

            }

            hitparticles.enableEmission = false;

            float t = (0.05f + (damage * 0.1f));

            if (t <= 0) t = 0.2f;

            yield return new WaitForSeconds(t);

            while (Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize != camsize)
            {
                Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(Cam.transform.GetChild(0).GetComponent<Camera>().orthographicSize, camsize, 0.07f);
                Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, new Vector3(0, 0, -10), 0.2f);
                yield return new WaitForEndOfFrame();
            }


            Time.timeScale = 1f;

            takingdamage = false;

        }
        else
        {

            hitparticles.enableEmission = true;

            hp -= damage;

            yield return new WaitForSeconds(0.1f);

            hitparticles.enableEmission = false;
        }


        if (hp <= 0)
        {
            print("Hp is less than 1, destroying player");


            Time.timeScale = 1f;
            ExitScreen.SetActive(true);
            Destroy(gameObject);
        }

    }

    public void SetGun(string G)
    {


        foreach (Transform child in gun.transform)
        {

            if (child.name == G) child.gameObject.SetActive(true);
            else child.gameObject.SetActive(false);

        }

    }


    public void SetSideGun(string G, bool right = true)
    {
        Transform ToChange;
        if (right) ToChange = sidegun2;
        else ToChange = sidegun1;

        foreach (Transform child in ToChange)
        {

            if (child.name == G) child.gameObject.SetActive(true);
            else child.gameObject.SetActive(false);

        }

    }






    public void SetTail(string T)
    {

        foreach (Transform child in tail.transform)
        {

            if (child.name == T) child.gameObject.SetActive(true);
            else child.gameObject.SetActive(false);

        }


    }

}
