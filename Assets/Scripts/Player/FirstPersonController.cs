using UnityEngine;
//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
//#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]

//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
//#endif

	public class FirstPersonController : MonoBehaviour
	{
        #region Variables
        [Header("Player")]
		public float MoveSpeed = 4.0f;
		public float SprintSpeed = 6.0f;
		public float RotationSpeed = 1.0f;
		public float SpeedChangeRate = 10.0f;

		[Header("Cinemachine")]
		public GameObject CinemachineCameraTarget;
		public float TopClamp = 90.0f; //que tanto puede moverse la camara
        public float BottomClamp = -90.0f;
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
	
		private PlayerInput _playerInput;
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _Min = 0.01f; //magnitud minima para que se considere mover la cámara

        #endregion

        private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			_playerInput = GetComponent<PlayerInput>();
		}

		private void Update()
		{
			Move();
		}

		private void LateUpdate() //LateUpdate para que primero esté todo y note objetos que se movieron de lugar
		{
			CameraRotation();
		}

        #region Code
        private void CameraRotation()
		{
			if (_input.look.sqrMagnitude >= _Min)
			{
				//si se usa mouse, no multiplica a tiempo real porque ya lo tiene incorporado
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				//para que sea más smooth la rotacion de la cámara y tenga límites
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				//para rotar la vista del player
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// si se sprintea, velocidad de sprint; si no, velocidad normal
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			if (_input.move == Vector2.zero) targetSpeed = 0.0f; //si player no se mueve, velocidad 0

			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// cambio de velocidad más orgánico
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
			if (_input.move != Vector2.zero) //ajusta la dirección del jugador
            {
                inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}
		
		private static float ClampAngle(float Angle, float Min, float Max)
		{
			if (Angle < -360f) Angle += 360f;
			if (Angle > 360f) Angle -= 360f;
			return Mathf.Clamp(Angle, Min, Max); //para que la camara no se pase del angulo deseado
		}
        #endregion
    }
}