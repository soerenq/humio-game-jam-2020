using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaldoGame : MonoBehaviour
{
     [SerializeField] private List<Image> _uiOwls = default;

     [SerializeField] private GameObject _rewardPopup;

     private int _score = 0;

     public void ClickOwl(GameObject owl)
     {
          owl.SetActive(false);
          _score++;
          for (var i = 0; i < _uiOwls.Count; i++)
          {
               Image image = _uiOwls[i];
               image.color = _score > i ? Color.white : Color.black;
          }

          if (_score >= 3)
          {
               //show reward popup
               _rewardPopup.SetActive(true);
               StartCoroutine(FadeIn(_rewardPopup.GetComponent<CanvasGroup>()));
          }
     }

     private IEnumerator FadeIn(CanvasGroup group)
     {
          float t = 0;
          float duration = 0.5f;
          while (t < duration)
          {
               group.alpha = t / duration;
               t += Time.deltaTime;
               yield return null;
          }

          group.alpha = 1;

     }
     private void Start()
     {
          _rewardPopup.SetActive(false);
     }
}