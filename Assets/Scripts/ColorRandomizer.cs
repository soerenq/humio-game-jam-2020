using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ColorRandomizer : MonoBehaviour
{
    [Button]
    void Colorize()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }
}
