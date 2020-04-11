using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Humio
{
    public class Navigator : MonoBehaviour
    {
        [SerializeField] private Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(()=>
            {
                if (SceneManager.GetActiveScene().name == "Overview")
                {
                    Console.Instance.ReplaceText("You try to go back, but it doesn't make sense here");
                }
                else
                {
                    Console.Instance.ReplaceText("You go back to Overview");
                    SceneManager.LoadScene("Overview");
                }
            });
        }
    }
}