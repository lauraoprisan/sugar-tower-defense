using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
 

    public static Player Instance { get; private set; }
    private float timeSinceLastShot = 0;
    [SerializeField] private float attackInterval = 0.4f;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if(Instance !=this) {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(0) && timeSinceLastShot > attackInterval) {
            Shoot();
            timeSinceLastShot = 0f;   
        }
    }

    private void Shoot() {
         Instantiate(ArmsSystem.Instance.currentArm, transform.position, transform.rotation);
    }

}