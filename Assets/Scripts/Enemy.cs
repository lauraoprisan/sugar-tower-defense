using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] private int attackPower;


    //this is for the defenders
    private void OnCollisionEnter2D(Collision2D collision) {
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();

        if (attackable != null) {
            attackable.TakeDamage(attackPower);
            Destroy(gameObject);
        }
        
    }
}
