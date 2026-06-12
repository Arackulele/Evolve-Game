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

public static class Extensions
{


    public static T GetRandomItem<T>(this List<T> self)
    {
        if (self.Count > 0) return self[UnityEngine.Random.Range(0, self.Count)];
        else
        {

            return default(T);

        }
    }


}
