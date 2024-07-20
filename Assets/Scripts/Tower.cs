using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public static Tower Instance{ get; private set; }
    private float health = 3f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health > 0) {
            EventManager.Instance.TowerDamage();
            Debug.Log("Tower is attacked. Health: " + health);
        } else {
            EventManager.Instance.TowerDestroyed();
            Destroy(gameObject);
        }
    }
}
