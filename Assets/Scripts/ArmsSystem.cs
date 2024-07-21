using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public enum Colors {
    Red,
    Blue,
    Green
}

[System.Serializable]
public class ArmType {
    public GameObject armPrefab;
    public Colors color;
}


public class ArmsSystem : MonoBehaviour {
    public static ArmsSystem Instance { get; private set; }

    public List<ArmType> armPrefabs = new List<ArmType>();
    public GameObject currentArm;
    private int currentArmIndex;
    private int numberOfArms;

    private void Awake() {
        if (Instance == null) {
            Debug.Log("arms systems initiates singletn");
            Instance = this;
        } else {
            Debug.Log("arms systems destroys instance");
            Destroy(Instance);
        }
    }

    private void Start() {

        if (armPrefabs.Count > 0) {
            currentArm = armPrefabs[0].armPrefab;
            currentArmIndex = 0;
            numberOfArms = armPrefabs.Count;
        } else {
            Debug.LogError("No arm prefabs assigned!");
        }
    }


    private void Update() {
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            
            if (scroll > 0f) {
                currentArmIndex = (currentArmIndex + 1) % numberOfArms;
            } else if (scroll < 0f) {
                currentArmIndex = (currentArmIndex - 1 + numberOfArms) % numberOfArms;
            }

            if (scroll != 0) {
                ChangeArm(currentArmIndex);
            }
            
        }
    }
    
    private void ChangeArm(int armIndex) {
        currentArm = armPrefabs[armIndex].armPrefab;
        EventManager.Instance.WeaponChange(armPrefabs[armIndex].color);
    }
    
}