using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CooldownManager : MonoBehaviour
{
    Image image;


    private void Start()
    {
        image = GetComponent<Image>();
    }


    public void StartCooldown(float cooldownTime)
    {
        StartCoroutine(CoolingDown(cooldownTime));
    }

    private IEnumerator CoolingDown(float cdTime)
    {
        image.fillAmount = 0;
        float timer = 0;
        while (timer < cdTime)
        {
            timer += Time.deltaTime;
            image.fillAmount = Mathf.Clamp01(timer / cdTime);
            yield return null;
        }

        image.fillAmount = 1;
    }
}
