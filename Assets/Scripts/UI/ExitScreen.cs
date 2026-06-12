using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitScreen : MonoBehaviour
{

    public static int total = 0;

    public TMP_Text ScoreT;

    public TMP_Text PointsSpentT;

    public TMP_Text PointsT;

    public TMP_Text TotalT;

    public List<AudioSource> Sound;

    public AudioClip Blip;

    public AudioClip RewardBlip;

    public static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        print("Exitscreen script");

        Sound = new List<AudioSource>();

        Sound.AddRange(GameObject.FindWithTag("Sound").GetComponents<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {


        if (InputSystem.actions.FindAction("XButton").triggered) SceneManager.LoadScene("TitleScreen");


    }

    private void OnEnable()
    {
        populate();
    }

    public void populate()
    {

        total = (StaticVars.highscore / 5) + StaticVars.spentmoney + (StaticVars.money * 2);

        StaticVars.metamoney += total;

        StartCoroutine(coolanimstuff());
        
    }

    private IEnumerator coolanimstuff()
    {

        yield return new WaitForSeconds(1f);

        yield return countuptonumber(StaticVars.highscore, ScoreT);

        Sound.GetRandomItem().PlayOneShot(RewardBlip);

        yield return new WaitForSeconds(0.4f);

        yield return countuptonumber(StaticVars.spentmoney, PointsSpentT);

        Sound.GetRandomItem().PlayOneShot(RewardBlip);

        yield return new WaitForSeconds(0.4f);

        yield return countuptonumber(StaticVars.money, PointsT);

        Sound.GetRandomItem().PlayOneShot(RewardBlip);

        yield return new WaitForSeconds(0.8f);

        yield return countuptonumber(total, TotalT);

        Sound.GetRandomItem().PlayOneShot(RewardBlip);

        yield break;
    }

    private IEnumerator countuptonumber(int num, TMP_Text disp)
    {

        int c = 0;
        while (c < num)
        {
            c++;
            disp.SetText(c.ToString());
            yield return new WaitForFixedUpdate();

            if (c % 10 == 0) Sound.GetRandomItem().PlayOneShot(Blip);
            if (c % 10 == 0) Sound.GetRandomItem().PlayOneShot(Blip);

        }


    }
}
