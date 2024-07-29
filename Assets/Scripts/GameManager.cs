using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject gameOverScreen;
    public GameObject healthRegenerationButton;

    //PowerUPs
    public HealthRegenerationPowerUp healthRegenerationPowerUp;


    void Start()
    {
        EventManager.Instance.OnWeaponChange += Instance_OnWeaponChange;
        EventManager.Instance.OnTowerDamage += Instance_OnTowerDamage;
        EventManager.Instance.OnTowerDestroyed += Instance_OnTowerDestroyed;
        EventManager.Instance.OnEnemyDestroyed += Instance_OnEnemyDestroyed;
        EventManager.Instance.onHealthRegenerationActivated += Instance_onHealthRegenerationActivated;
        healthSlider.maxValue = Tower.Instance.maxHealth;
        healthSlider.value = Tower.Instance.maxHealth;
    }

    private void Instance_onHealthRegenerationActivated(object sender, EventArgs e) {
        Debug.Log("should substract from xp: " + healthRegenerationPowerUp.xpNeeded);
;        totalXP -= healthRegenerationPowerUp.xpNeeded;
        XPText.text = totalXP.ToString();
    }

    private void Instance_OnEnemyDestroyed(object sender, EventManager.EnemyDestroyedEventArgs e) {
        totalXP += e.xpToAdd;
        XPText.text = totalXP.ToString();

        //make the power up be possible to activate
        if (totalXP >= healthRegenerationPowerUp.xpNeeded && !healthRegenerationPowerUp.hasBeenUsedOnce) { //but more then powerup.xpNeeded
            healthRegenerationPowerUp.canActivate = true;
        }
    }

    private void Instance_OnTowerDamage(object sender, EventArgs e) {
        healthSlider.value = Tower.Instance.health;
    }

    private void Update() {
        healthSlider.value = Tower.Instance.health;

        if (healthRegenerationPowerUp.canActivate) {
            healthRegenerationButton.GetComponent<Image>().color = Color.white;
        } else {
            healthRegenerationButton.GetComponent<Image>().color = Color.gray;
        }
    }

    private void Instance_OnWeaponChange(object sender, WeaponChangeEventArgs e) {
        SetOutline(e.Color);
    }


    void SetOutline(Colors color) {
        foreach (ImageContainerType imageContainerType in imageContainers) {
            var outline = imageContainerType.imageContainer.GetComponent<Outline>();

            if (outline != null && imageContainerType.color == color) {
                outline.enabled = true;
            } else {
                outline.enabled = false;
            }
        }
    }

    private void Instance_OnTowerDestroyed(object sender, EventArgs e) {
        gameOverScreen.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy() {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnWeaponChange -= Instance_OnWeaponChange;
            EventManager.Instance.OnTowerDamage -= Instance_OnTowerDamage;
            EventManager.Instance.OnTowerDestroyed -= Instance_OnTowerDestroyed;
            EventManager.Instance.OnEnemyDestroyed -= Instance_OnEnemyDestroyed;
            EventManager.Instance.onHealthRegenerationActivated -= Instance_onHealthRegenerationActivated;
        }
    }
}
