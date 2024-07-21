using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageContainerType {
    public GameObject imageContainer;
    public Colors color;
}

// Events' arguments
public class WeaponChangeEventArgs : EventArgs {
    public Colors Color { get; }

    public WeaponChangeEventArgs(Colors color) {
        Color = color;
    }
}

public class GameManager : MonoBehaviour{

    //UI elements
    public List<ImageContainerType> imageContainers = new List<ImageContainerType>();
    public Slider healthSlider;
    public Text XPText;
    public int totalXP;
       
    void Start()
    {
        EventManager.Instance.OnWeaponChange += Instance_OnWeaponChange;
        EventManager.Instance.OnTowerDamage += Instance_OnTowerDamage;
        EventManager.Instance.OnEnemyDestroyed += Instance_OnEnemyDestroyed;
        healthSlider.maxValue = Tower.Instance.maxHealth;
        healthSlider.value = Tower.Instance.maxHealth;
    }

    private void Instance_OnEnemyDestroyed(object sender, EventManager.EnemyDestroyedEventArgs e) {
        totalXP += e.xpToAdd;
        XPText.text = totalXP.ToString();
    }

    private void Instance_OnTowerDamage(object sender, EventArgs e) {
        healthSlider.value = Tower.Instance.health;
    }

    private void Instance_OnWeaponChange(object sender, WeaponChangeEventArgs e) {
        Debug.Log("Arm color from the event is: " + e.Color);
        SetOutline(e.Color);
    }


    void SetOutline(Colors color) {
        foreach (ImageContainerType imageContainerType in imageContainers) {
            Debug.Log("this imageContainerType color is: " + imageContainerType.color);
            var outline = imageContainerType.imageContainer.GetComponent<Outline>();

            if (outline != null && imageContainerType.color == color) {
                outline.enabled = true;
            } else {
                outline.enabled = false;
            }
        }
    }
 
}
