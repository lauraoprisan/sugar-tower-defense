using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;



    void FixedUpdate() {
       
        rb.velocity = Vector2.left * speed;
        
    }
}
