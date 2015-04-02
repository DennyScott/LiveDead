using UnityEngine;
using System.Collections;

public abstract class Character : Grunt {

    public int health;
    public float speed = 5.0f;
    public int gcd;

    public delegate void characterAction(GameObject g);
    public event characterAction onAttack;
    public event characterAction onDamage;


    #region Event Triggers
    private void TriggerOnAttack () {
        if(onAttack != null) {
            onAttack(gameObject);
        }
    }

    private void TriggerOnDamage () {
        if(onDamage != null) {
            onDamage(gameObject);
        }
    }
    #endregion

    public void TakeDamage (int amount) {
        health -= amount;
        TriggerOnDamage();
    }
}
