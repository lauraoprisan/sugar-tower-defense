using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float horizontalInput;
    public float verticalInput;
    public float playerSpeed;
    private float minXLimit = -4.6f;
    private float maxXLimit = 8.4f;
    private float maxYLimit = 0.5f;
    private float minYLimit = -4.5f;



    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * playerSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector2.up * playerSpeed * verticalInput * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minXLimit, maxXLimit);
        float clampedY = Mathf.Clamp(transform.position.y, minYLimit, maxYLimit);
        transform.position = new Vector2(clampedX, clampedY);
    }
}