using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public static Tower Instance{ get; private set; }
    public float maxHealth = 25;
    public float health;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() { 
        health=maxHealth;
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
