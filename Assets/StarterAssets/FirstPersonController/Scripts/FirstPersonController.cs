using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

	
#if ENABLE_INPUT_SYSTEM
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

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
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
			Grab();
			//get clicks?
			//GrabItem();
		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}


        
        
		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
        }

		


		private void Move()
		{

			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		RaycastHit DetectHit(Vector3 startPos, float distance, Vector3 direction)
		{
			//init ray to save the start and direction values
			Ray ray = new Ray(startPos, direction);
			//varible to hold the detection info
			RaycastHit hit;
			//the end Pos which defaults to the startPos + distance 
			Vector3 endPos = startPos + (distance * direction);
			if (Physics.Raycast(ray, out hit, distance))
			{
				//if we detect something
				endPos = hit.point;
			}
			// 2 is the duration the line is drawn, afterwards its deleted
			//Debug.DrawLine(startPos, endPos, Color.green, 2);
			return hit;
		}

		// [SerializeField GameObject heldItem]
		[Space(10)]
		[Header("GrabItem")]
		[SerializeField] GameObject item;
		private bool oldLeftclick;


		private void Grab(){
			//check Raycast?
			//PUT ON GRABBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
			
			var hit = DetectHit(CinemachineCameraTarget.transform.position, 10f, CinemachineCameraTarget.transform.forward);
			//print(_input.leftclick);
			// print(_input.sprint);

			//I'm looking at the object
			//print(_input.leftclick);
			//tempItem.transform.position = hit.point	;
			if ((oldLeftclick == false) && (_input.leftclick == true)){
				//return

				//print(_input.leftclick);
				//print("TRYING TO GRAB");
				// print(hit.GetPoint(hit.direction));
				// print(hit.GetPoint(hit.direction));
				//print(hit.point);
				//hit.
				
				//print(hit.transform.position);

				//I hit something with the raycast
				if (hit.transform != null){
					//code in functionality for: (both should contain some if statements in here)
					// coins
					// bottles

					//hit collider transport
					//if (hit.transform.tag == "Scotty"){
					//	print("beam me up");
		
					//	return;
					//}

					
					//I AM HOLDING AN ITEM (THUS YEET IT)
					// if (item.transform.parent == hit.transform.parent){
					if (item.transform.childCount != 0){
						//DROP ITEM
						print("trying to drop");
						//if tag bottle
						// if (hit.transform.gameObject.tag == "Grab"){
						// 	//if(Key.KeyType.Coke == hit.transform.gameObject.GetComponent<Key>().type){
						// 	if(Key.KeyType.Coke == hit.transform.gameObject.GetComponent<Key>().type){

						// 	}
						// }

						//FOR COIN DO SOMETHING WACK


						//if its coin, and I am holding coin, merge


						//DO GRAB MORE ITEMS/N STUFF HERE
						
						//item.transform.SetParent(null);
						//print(item.transform.GetChild(0).transform.position);

						// item.transform.GetChild(0).transform.position = hit.transform.position;
						// if(hit.transform.gameObject.GetComponent("GrabCoin.cs") != null){

						//OTHER ITEM IS COIN
						if(hit.transform.gameObject.GetComponent<GrabCoin>() != null){
							print("it have it");
							if (item.transform.GetChild(0).transform.gameObject.GetComponent<GrabCoin>() != null){
								print("i am holding coin too");


								//check if i added a coin (if i have 4 this should not activate)
								int coinNum = hit.transform.GetComponent<GrabCoin>().GetCoinNum();
								if (item.transform.GetChild(0).transform.gameObject.GetComponent<GrabCoin>().AddCoin(coinNum)){
									//yeet the other coin
									print("should bee yeeting coin");
									Destroy(hit.transform.gameObject);
									return;
								}
							}
							// if (hit.transform.gameObject.GetComponent<GrabCoin>().AddCoin()){
							// 	//yeet the other coin
							// 	print("should bee yeeting coin");
							// 	Destroy(hit.transform.gameObject);
							// 	return;
							// }		
						}//else{
						//	print("say it like you mean it");
						//}

						//OTHER ITEM IS COKE OR LAXATIVES
						if (hit.transform.gameObject.tag == "Golden Idol"){
							Key.KeyType heldItemKeyType = item.transform.GetChild(0).transform.gameObject.GetComponent<Key>().type;
							// if (item.transform.GetChild(0).transform.gameObject.GetComponent<Key>().type == Key.KeyType.Coke)
							if (heldItemKeyType == Key.KeyType.Coke || heldItemKeyType == Key.KeyType.Energy || heldItemKeyType == Key.KeyType.Lax){
								//do switch (just requires the grppable tag lol)
								hit.transform.gameObject.tag = "Grab";

								//make dropped item ungrabbable
								//item.transform.GetChild(0).GameObject().transform.tag = "Untagged";

								//item.transform.GetChild(0).GameObject().transform.tag = "Sand Bag";
								//item.transform.GetChild(0).tag = "Sand Bag";
								item.transform.GetChild(0).gameObject.tag = "Sand Bag";
								// print(item.transform.GetChild(0).tag);
								// print(item.transform.GetChild(0).tag);
								// print(item.transform.GetChild(0).tag);
								// print(item.transform.GetChild(0).tag);
								// print(item.transform.GetChild(0).tag);
								// print(item.transform.GetChild(0).GameObject().transform.tag);
								// print(item.transform.GetChild(0).GameObject().transform.tag);
								// print(item.transform.GetChild(0).GameObject().transform.tag);

								//print("qwerty is missed");	
							}


							//Debug statement
								//print("ur golden bro");
						}
						

						//DROP ITEM
						item.transform.GetChild(0).transform.position = hit.point;
						// item.transform.GetChild(0).transform.position = hit.point + Vector3.up*0.2f;
						item.transform.GetChild(0).GameObject().layer = 0;
						item.transform.GetChild(0).SetParent(null);


						//DO SWITCHEROO (SWTICHING BOTTLES)
						//GRAB ITEM
						// print("hands empty i swaer");
						// if (hit.transform.gameObject.tag == "Grab"){
						// 	//hold
						// 	//hit.transform.SetParent(item.transform.parent);
						// 	hit.transform.SetParent(item.transform);
						// 	hit.transform.position = item.transform.position;
						// 	hit.transform.GameObject().layer = 2;
						// }
						
						

						//throw data bad boi
						if (!hit.transform.GetComponent<Rigidbody>().isKinematic){
							//hit.transform.GetComponent<Rigidbody>().velocity = hit.transform.forward * 10f;
							hit.transform.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
						}
						
						
					}//HANDS EMPTY. TIME TO GRAB!!!!!!!!!!!!
					if (item.transform.childCount == 0){
					// }else{ //HANDS EMPTY. TIME TO GRAB!!!!!!!!!!!!
						//GRAB ITEM
						print("hands empty i swaer");
						if (hit.transform.gameObject.tag == "Grab"){
							//hold
							//hit.transform.SetParent(item.transform.parent);
							hit.transform.SetParent(item.transform);
							hit.transform.position = item.transform.position;
							hit.transform.GameObject().layer = 2;
							//enable raycast!
						}

					}


				}




				//if there is as raycast
				// if (hit.transform != null){
				// 	if (hit.transform.gameObject.tag == "Grab"){
				// 		//print("FOUND GRABBING ITEEMMMMM");
				// 		//object1.transform.SetParent(object2 .transform.parent);

				// 		//i am holding item (this is messed up)
				// 		if (item.transform.parent == hit.transform.parent){
				// 			//drop
				// 			hit.transform.SetParent(null);
				// 			//throw data bad boi
				// 			if (!hit.transform.GetComponent<Rigidbody>().isKinematic){
				// 				//hit.transform.GetComponent<Rigidbody>().velocity = hit.transform.forward * 10f;
				// 				hit.transform.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
				// 			}
							
							
				// 		}else{
				// 			//hold
				// 			hit.transform.SetParent(item.transform.parent);
				// 			hit.transform.position = item.transform.position;
				// 			//hit.transform.GetComponent<Rigidbody>().velocity = hit.transform.forward * 10f;
				// 		}
				// 	//hit.transform.SetParent(item.transform.parent);

				// 	}
					
				// }
			}
			
				// if (_input.leftclick){
				// if (_input.leftclickPress){
				//if ((_input.leftclickPress == false) || (_input.leftclick  == true)){
				
			

			//hardcode stuff
			oldLeftclick = _input.leftclick;

		}


		//OTHER SHIT

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}


		//[SerializeField GameObject item]
		/*public void GrabItem()
		{
			Debug.Log(_input.leftclick)

		}*/
	}
}