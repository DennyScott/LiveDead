using UnityEngine;
using System.Collections;

public class Attack : Grunt {

    private enum CharacterStates { ATTACK, NEUTRAL };
    private CharacterStates currentState = CharacterStates.NEUTRAL;

	#region Public Variables
	public float _attackRange = 1.0f;
    public float _hitbox = 1.0f;
	#endregion

	#region Private Variables
	Attack _target;
	#endregion

	#region delegates
	public System.Action<GameObject> onAttackState;
    public System.Action<GameObject> onAttack;
	#endregion

	#region MonoBehaviour Methods
	void Update() {
		
    }
	#endregion

	#region Private Methods
	void AutoAttack() {
        if(currentState == CharacterStates.ATTACK) {
            if(IsTargetInAttackRange(_attackRange) && IsTargetInView(50.0f)) {
                Debug.Log("Attacking Target");
            }
        }
    }

	bool IsTargetInAttackRange(float attackRange) {
		return Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackRange + _target._hitbox;
	}

	bool IsTargetInView(float allowableAngle) {
		return Vector3.Angle(gameObject.transform.forward, _target.transform.position - gameObject.transform.position) < allowableAngle;
	}
	#endregion

	#region Public Methods
	public void AttackTarget(Attack target) {
        _target = target;
        TriggerAttackState();
    }
	#endregion

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

	#region Coroutines
	IEnumerator CharacterState() {
		switch(currentState) {
			case CharacterStates.ATTACK:
				yield AutoAttack();
				break;
		}
	}
	#endregion
}
