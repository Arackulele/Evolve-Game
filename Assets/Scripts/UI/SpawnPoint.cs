using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayWarn()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        transform.GetChild(0).gameObject.SetActive(false);

        yield break;
    }
}
