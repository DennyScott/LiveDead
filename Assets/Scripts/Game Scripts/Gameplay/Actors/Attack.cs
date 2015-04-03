using UnityEngine;
using System.Collections;

public class Attack : Grunt {

    private enum CharacterStates { ATTACK, NEUTRAL };
    private CharacterStates currentState = CharacterStates.NEUTRAL;

    public float _attackRange = 1.0f;
    public float _hitbox = 1.0f;

    public delegate void characterState(GameObject g);
    public event characterState onAttackState;

    public delegate void characterAction(GameObject g);
    public event characterAction onAttack;

    Attack _target;

    void Start() {

    }

    void Update() {
        AutoAttack();
    }

    void AutoAttack() {
        if(currentState == CharacterStates.ATTACK) {
            if(IsTargetInAttackRange(_attackRange) && IsTargetInView(360.0f)) {
                Debug.Log("Attacking Target");
            }
        }
    }

	bool IsTargetInAttackRange(float attackRange) {
		return Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackRange + _target._hitbox;
	}

	bool IsTargetInView(float allowableAngle) {
		Debug.Log(Vector3.Angle(gameObject.transform.forward, _target.transform.position - gameObject.transform.position));
		return Vector3.Angle(gameObject.transform.forward, _target.transform.position - gameObject.transform.position) < allowableAngle;
	}

    public void AttackTarget(Attack target) {
        _target = target;
        TriggerAttackState();
    }

    #region Event Triggers
    private void TriggerOnAttack() {
        if(onAttack != null) {
            onAttack(gameObject);
        }
    }

    void TriggerAttackState() {
        currentState = CharacterStates.ATTACK;
        if(onAttackState != null) {
            onAttackState(gameObject);
        }
    }
    #endregion
}
