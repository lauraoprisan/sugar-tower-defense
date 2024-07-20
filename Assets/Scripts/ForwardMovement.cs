using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isTowardsTower = false;
    private float direction = 1;


    private void Start() {
        if (isTowardsTower) {
            direction = -1;
        }
    }


    void FixedUpdate() {
        rb.velocity = Vector2.right * speed * direction;

    }
}

