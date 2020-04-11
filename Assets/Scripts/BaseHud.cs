using System;
using UnityEngine;

namespace Humio
{
    public class BaseHud : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}