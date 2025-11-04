using System;
using System.Collections;
using System.Collections.Generic;
using ODG.UI.Animation;
using UnityEngine;

public class FadeElementTestComponent : MonoBehaviour
{
   [SerializeField] private  FadeGroups group;
    bool active = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active)
            {
                group.Hide();
                active = false;
            }
            else
            {
                group.Show();
                active = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (active)
            {
                group.SkipHide();
                active = false;
            }
            else
            {
                group.SkipShow();
                active = true;
            }
        }
    }
}
