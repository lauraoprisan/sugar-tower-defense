using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int attackPower;
    public Colors color;


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
            Debug.Log("collision.gameObject : " + collision.gameObject);
            Debug.Log("collision.gameObject.GetComponent<Projectile>() : " + collision.gameObject.GetComponent<Projectile>());
            //if the projectile.color === enemy.color then enemyTakesDamage and Destroy are called
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            Debug.Log("projectile.color " + projectile.color);
            Debug.Log("this.color " + this.color);


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

        if ((health <=0)) {
            Destroy(gameObject);
        } 
    }

}
