using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageContainerType {
    public GameObject imageContainer;
    public Colors color;
}


// Events' arguments
public class WeaponChangeEventArgs : EventArgs {
    public Colors Color { get; }

    public WeaponChangeEventArgs(Colors color) {
        Color = color;
    }
}

public class GameManager : MonoBehaviour{

    //UI elements
    public List<ImageContainerType> imageContainers = new List<ImageContainerType>();
       
    void Start()
    {
        EventManager.Instance.OnWeaponChange += Instance_OnWeaponChange;
    }
    private void Instance_OnWeaponChange(object sender, WeaponChangeEventArgs e) {
        Debug.Log("Arm color from the event is: " + e.Color);
        SetOutline(e.Color);
    }


    void SetOutline(Colors color) {
        foreach (ImageContainerType imageContainerType in imageContainers) {
            Debug.Log("this imageContainerType color is: " + imageContainerType.color);
            var outline = imageContainerType.imageContainer.GetComponent<Outline>();

            if (outline != null && imageContainerType.color == color) {
                outline.enabled = true;
            } else {
                outline.enabled = false;
            }
        }
    }
 
}
