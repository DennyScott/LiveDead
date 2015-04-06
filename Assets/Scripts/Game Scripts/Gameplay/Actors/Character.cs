using UnityEngine;

public abstract class Character : Grunt {

	#region Public Variables
	public int Health;
    public float Speed = 5.0f;
    public int Gcd;
	public enum CharacterStates { Neutral = 0, Attack = 1 };
	protected Character Target;

	//The two below values will be determined by the weapon later
	public float AttackRange = 1.0f;
	public float AttackView = 50.0f;
	public float Hitbox = 1.0f;
	#endregion

	#region Private Variables
	private CharacterStates _currentState;
	private readonly IState[] _characterState = new IState[System.Enum.GetNames(typeof(CharacterStates)).Length];
	#endregion

	#region Monobehaviour Methods

	protected virtual void Start() {
		//Store State for later use.
		_characterState[(int)CharacterStates.Neutral] = new NeutralState(this);
		_characterState[(int)CharacterStates.Attack] = new AttackState(this);
		CurrentState = CharacterStates.Neutral;
	}

	protected virtual void Update() {
		TriggerStateUpdate();
	}

	#endregion

	#region Properties
	public CharacterStates CurrentState {
		get {
			return _currentState;
		}
		set {
			TriggerStateExit();
			_currentState = value;
			TriggerStateChange();
			TriggerStateEnter();
			TriggerStateStart();
		}
	}
	#endregion

	#region Delegates
	public System.Action<GameObject> OnDamage;
	public System.Action<GameObject> OnStateChange;
	public System.Action<GameObject> OnStateExit;
	public System.Action<GameObject> OnStateEnter;
	public System.Action<GameObject> OnStateStart;
	public System.Action<GameObject> OnStateUpdate;
	#endregion



	#region Event Triggers
	void TriggerOnDamage () {
        if(OnDamage != null) {
            OnDamage(gameObject);
        }
    }

	void TriggerStateChange() {
		if(OnStateChange != null) {
			OnStateChange(gameObject);
		}
	}

	void TriggerStateEnter() {
			OnStateEnter += _characterState[(int)CurrentState].OnStateEnter;
			OnStateExit += _characterState[(int)CurrentState].OnStateExit;
			OnStateStart += _characterState[(int)CurrentState].OnStart;
			OnStateUpdate += _characterState[(int)CurrentState].OnUpdate;
			OnStateEnter(gameObject);	
	}

	void TriggerStateStart() {
		if(OnStateStart != null) {
			OnStateStart(gameObject);
		}
	}

	void TriggerStateUpdate() {
		if(OnStateUpdate != null) {
			OnStateUpdate(gameObject);
		}
	}

	void TriggerStateExit() {
		if (OnStateExit == null) {
			return;
		}
		OnStateExit(gameObject);
		OnStateEnter -= _characterState[(int)CurrentState].OnStateEnter;
		OnStateExit -= _characterState[(int)CurrentState].OnStateExit;
		OnStateStart -= _characterState[(int)CurrentState].OnStart;
		OnStateUpdate -= _characterState[(int)CurrentState].OnUpdate;
	}
    #endregion

	#region Public Methods
	public bool IsTargetInRange(float range) {
		return Vector3.Distance(gameObject.transform.position, Target.transform.position) <= range + Target.Hitbox;
	}

	public bool IsTargetInView(float allowableAngle) {
		return Vector3.Angle(transform.forward, Target.transform.position - transform.position) < allowableAngle;
	}

	public void TakeDamage (int amount) {
        Health -= amount;
        TriggerOnDamage();
	}
	#endregion
}
