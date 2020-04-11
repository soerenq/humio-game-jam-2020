using System;
using TMPro;
using UnityEngine;

namespace Humio
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private static Console _instance;

        public static Console Instance => _instance;
        

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddText(String text)
        {
            _text.text = text;
        }
    }
}