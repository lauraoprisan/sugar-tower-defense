using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
   public static EventManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    //Declaring the events

    public event EventHandler OnTowerDamage;
    public event EventHandler OnTowerDestroyed;
    public event EventHandler<WeaponChangeEventArgs> OnWeaponChange;
    public event EventHandler OnEnemyAttaked;
    public event EventHandler<EnemyDestroyedEventArgs> OnEnemyDestroyed;


    // Event Arguments
    public class EnemyDestroyedEventArgs : EventArgs {
        public int xpToAdd;

        public EnemyDestroyedEventArgs(int xpGained) {
            xpToAdd = xpGained;
        }
    }



    //Creating the functions to use for triggering the event

    public void TowerDamage() { 
        OnTowerDamage?.Invoke(this, EventArgs.Empty);
    }

    public void TowerDestroyed() { 
        OnTowerDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public void WeaponChange(Colors color) {
        OnWeaponChange?.Invoke(this, new WeaponChangeEventArgs(color));
    }

    public void EnemyDamaged() { 
        OnEnemyAttaked?.Invoke(this, EventArgs.Empty);
    }

    public void EnemyDestroyed(int xpGained) { 
        OnEnemyDestroyed?.Invoke(this,new EnemyDestroyedEventArgs(xpGained));
    }

}
