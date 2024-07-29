using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int attackPower;
    [SerializeField] public int xpPointsGiven;
    public Colors color;



    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Tower")) {
            Tower.Instance.TakeDamage(attackPower);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            //if the projectile.color === enemy.color then enemyTakesDamage and Destroy are called
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();


            // check if the projectile's color matches the enemy's color
            if (projectile != null) {
                if (projectile.color == this.color) {
                    EnemyTakesDamage(projectile.attackPower);
                    Destroy(collision.gameObject);
                }


            }
        }
    }

    private void EnemyTakesDamage(int pojectileAttackPower) {
        health -= pojectileAttackPower;

        if (Tower.Instance.health > 0) { 
        
            if ((health <= 0)) {
                EventManager.Instance.EnemyDestroyed(this.xpPointsGiven);
                Destroy(gameObject);
            } else {
                EventManager.Instance.EnemyDamaged();
            }

        }

    }
}
