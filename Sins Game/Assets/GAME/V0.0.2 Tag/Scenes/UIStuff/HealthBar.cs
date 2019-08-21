using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image currentCooldownBar;
    public Text ratioText;

    private float hitpoint = 150;
    private float maxHitpoint = 150;

    private void Start()
    {
        
    }

    private void UpdateCooldownBar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentCooldownBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString() + '%';
    }
//use ability
    private void ReduceBar(float damage)
    {
        hitpoint -= damage;
        
    }
//increase bar slowly so ability can be used again
    private void IncreaseBar(float health)
    {
        
    }
}