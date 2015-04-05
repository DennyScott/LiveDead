using System;
using UnityEngine;

public class Player : Character {
	public GameObject Enemy;
	private Character _tempTarget;

	protected override void Start() {
		base.Start();
		_tempTarget = Enemy.GetComponent<Character>();
	}

	protected override void Update() {
		base.Update();
		var currentSpeed = GetCurrentSpeed();

		if(Input.GetButton("Fire2")) {
			Debug.Log("Setting Attack");
			CurrentState = CharacterStates.Attack;
			Target = _tempTarget;
		}

		var translation = Input.GetAxis("Vertical") * currentSpeed;
		var rotation = Input.GetAxis("Horizontal") * currentSpeed;

		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate(0, translation, 0);
		transform.Translate(rotation, 0, 0);
	}

	/// <summary>
	///     If a player is running at both angles, don't speed the character
	///     up by double the speed. Instead, we allow them to go at 1.5 times the pace.
	///     If only one is down, return using the regular speed calculation.
	/// </summary>
	/// <returns>float speed to move by</returns>
	private float GetCurrentSpeed() {
		const int tolerance = 0;
		return Math.Abs(Input.GetAxis("Vertical")) > tolerance && Math.Abs(Input.GetAxis("Horizontal") - tolerance) > tolerance ? Speed / 1.5f : Speed;
	}
}