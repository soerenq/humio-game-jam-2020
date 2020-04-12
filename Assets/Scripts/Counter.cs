using System;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Humio
{
    public class Counter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _counterTxt;
        private float _startTime;

        private static Counter _instance;

        public static Counter Instance => _instance;
        

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }

        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == "Overview")
            {
                StartTimer();
            }

            SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        }

        public void StartTimer()
        {
            _startTime = Time.time;
        }

        public void AddPenalty(float seconds)
        {
            _startTime -= seconds;
        }
        private void Update()
        {
            var t = Time.time - _startTime;
            _counterTxt.text = $"{(int)t/60} : {(t % 60):F1}";
        }
    }
}