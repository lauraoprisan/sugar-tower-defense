using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int attackPower;
    private int projectilePowerAttack = 1; //temporary solution for projectile power attack


    //this is for the defenders
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Tower")) {
            Tower.Instance.TakeDamage(attackPower);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile")) {
            Debug.Log("enemy attacked");

            //check what kinde of projectile is
            GameObject projectile = collision.gameObject;

            //if the projectile.color === enemy.color then enemyTakesDamage and Destroy are called
            EnemyTakesDamage(projectilePowerAttack);
            Destroy(collision.gameObject);
        }
    }

    private void EnemyTakesDamage(int projectilePowerAttack) {
        health -= projectilePowerAttack;

        if ((health <=0)) {
            Destroy(gameObject);
        } 
    }

}
