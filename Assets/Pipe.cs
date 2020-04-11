﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
   public Sprite EmptyPipe;
   public Sprite FilledPipe;

   private SpriteRenderer pipeSprite;

   private void Awake() {
       pipeSprite = GetComponentInChildren<SpriteRenderer>();
   }

   public void ToggleSprite(bool filled) {
       if (filled) {
           pipeSprite.sprite = FilledPipe;
       }
       else {
           pipeSprite.sprite = EmptyPipe;
       }
   }
}
