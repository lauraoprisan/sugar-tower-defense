using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float xRange = 12f;


    void Update() {
        if (transform.position.x < -xRange || transform.position.x > xRange) {
            Destroy(gameObject);
        }
    }
}
