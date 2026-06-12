using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    Vector3 originalPos;

    void Awake()
    {

        originalPos = transform.position;

    }


    void FixedUpdate()
    {

        if (CharacterController2D.Instance) transform.position = Vector3.MoveTowards(transform.position, (CharacterController2D.Instance.transform.position / 11 ), 0.04f);

    }
}