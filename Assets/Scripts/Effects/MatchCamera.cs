using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCamera : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    gameObject.GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
        
    }
}
