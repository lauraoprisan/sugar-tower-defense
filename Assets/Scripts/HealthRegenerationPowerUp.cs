using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum PowerUpType { 
    HealthRegeneration
}
public class HealthRegenerationPowerUp : PowerUp
{
    public PowerUpType powerUpType= PowerUpType.HealthRegeneration;
    public int xpNeeded;
    public bool canActivate;
    public bool hasBeenUsedOnce = false;

    private Coroutine regenerationCoroutine;


    private void Start() {
        EventManager.Instance.OnTowerDestroyed += Instance_OnTowerDestroyed;
    }

    private void Instance_OnTowerDestroyed(object sender, System.EventArgs e) {
        Deactivate();  //deactivate coroutine
    }

    public override void Activate() {
        if (!canActivate) return;

        hasBeenUsedOnce = true;
        canActivate = false;
        EventManager.Instance.HealthRegenerationActivated();
        Debug.Log("power was activated");
        if (regenerationCoroutine == null) {
            regenerationCoroutine = StartCoroutine(HealthRegenerationRoutine());
        }
    }

    private IEnumerator HealthRegenerationRoutine() {
        while (true) {
            if (Tower.Instance.health <25) { 
                Tower.Instance.health += 1; 
            }

            Debug.Log("Health regenerated to: " + Tower.Instance.health);
            yield return new WaitForSeconds(0.7f);
        }
    }


    public void Deactivate() {
        if (regenerationCoroutine != null) {
            StopCoroutine(regenerationCoroutine);
            regenerationCoroutine = null;
        }
    }

}
