using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour {

	public Button btnUp;
	public Button btnDown;
	public Button btnShoot;
	public GameObject DirectionIndicator;

	public ParticleSystem PFX_LunteDots;
	public ParticleSystem PFX_LunteStripes;
	public GameObject kanonMan;
	public GameObject kanonManLunte;
	public GameObject PfxHittingGround;
	Rigidbody rigidBody;
	bool isReadyToCollide;
	bool isReadyToJump;
	bool isJumping;
	bool isJumpingAnimationActivated;
	bool isDeparted;
	bool isCountingDownForJump;
	bool isBtnUp_down;
	bool isBtnDown_down;
	public bool isGameOn;
	float jumpCountdown = 0.6f;
	float jumpShootCountdown = 1.2f;
	float jumpRotationSpeed = 0.3f;
	float jumpRotation = 180;
	float jumpRotationIndicatorNum = 45;
	float jumpRotationIndicatorRotationSpeed = 0.5f;
	Vector3 posWhenShooting;

	public CameraMan camScript;

	// Use this for initialization
	void Start () 
	{
		// Set listeners for UI buttons
		btnShoot.onClick.AddListener (() 	=> { pressedShot();	});

		// Position right before shooting
		Vector3 pos = transform.position;
		float posY = 0.9f + pos.y; 	// Upward/Downward
		float posZ = -1.2f + pos.z; // Forward/Backward
		posWhenShooting = new Vector3 (0, posY, posZ);
	}

	public void btnUpDown (){ isBtnUp_down = true;}
	public void btnDownDown () { isBtnDown_down = true;}
	void UpdateMouseDown ()
	{
		// Reset bools regarding mouse down on mouse release
		if (Input.GetMouseButtonUp (0)) {
			ResetTiltingUsingMouse();
		}

		// Determine new rotation up-wards
		if (isBtnUp_down || isKeyboardBtnDown_down) 
		{
			if (jumpRotationIndicatorNum < 90)
				jumpRotationIndicatorNum += jumpRotationIndicatorRotationSpeed;
			if (jumpRotationIndicatorNum > 90)
				jumpRotationIndicatorNum = 90;
		}

		// Determine new rotation down-wards
		if (isBtnDown_down || isKeyboardBtnUp_down) 
		{
			if (jumpRotationIndicatorNum > 0)
				jumpRotationIndicatorNum -= jumpRotationIndicatorRotationSpeed;
			if (jumpRotationIndicatorNum < 0)
				jumpRotationIndicatorNum = 0;
		}

		// Set the new rotation for when he will jump
		jumpRotation = -(90 + jumpRotationIndicatorNum);

		// Update the rotation indicator
		//transform.root.rotation = Quaternion.Euler(new Vector3(jumpRotation, 0, 0));
		float xRotation = 180 - (Mathf.Abs (jumpRotation)); // Mathf.Abs(transform.root.rotation.x) * 360 + 180;

		//transform.root.rotation = Quaternion.Euler(new Vector3(jumpRotation, 0, 0));
		DirectionIndicator.transform.localRotation = Quaternion.Euler(0, 45, xRotation);
	}

	void pressedShot ()
	{
		// Remove Listener
		btnShoot.onClick.RemoveAllListeners();

		// Push Sign back
		float PushPower = Random.Range (100,200);
		rigidBody = transform.gameObject.AddComponent<Rigidbody> ();
		rigidBody.AddForce (new Vector3(-PushPower, 0, PushPower));

		// Play Smack Sound
		int soundNum = Random.Range (0,3);
		if(soundNum == 0) 		GetComponent<SoundsTriggered>().playSound1(true, 1);
		else if (soundNum == 1)	GetComponent<SoundsTriggered>().playSound2(true, 1);
		else if (soundNum == 2) GetComponent<SoundsTriggered>().playSound3(true, 1);

		// Flag CollisionAble
		isReadyToCollide = true;

		// Flag animation to play and do a-da Jump'a
		isReadyToJump = true;

		// Hide Indicator
		DirectionIndicator.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDepartmentJump ();
		UpdateMouseDown ();
		UpdateKeyboardUsage ();
	}

	private bool isKeyboardBtnUp_down;
	private bool isKeyboardBtnDown_down;
	void UpdateKeyboardUsage ()
	{
		if (isReadyToJump == false && isDeparted == false) 
		{
			// Pressing space starts the shot
			if (Input.GetKeyUp (KeyCode.Space)) {
				pressedShot ();
			}

			// Tilt kanon up
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				isKeyboardBtnUp_down = true;
			}
			// Tilt kanon down
			else if (Input.GetKeyDown (KeyCode.UpArrow)) {
				isKeyboardBtnDown_down = true;
			}
			// Else stop tilting..
			else if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.DownArrow))
			{
				ResetTiltingUsingKeyboard();
			}
		}
	}

	void ResetTiltingUsingMouse()
	{
		isBtnUp_down = false;
		isBtnDown_down = false;
	}
	
	void ResetTiltingUsingKeyboard()
	{
		isKeyboardBtnUp_down = false;
		isKeyboardBtnDown_down = false;
	}

	void UpdateDepartmentJump ()
	{
		if (isReadyToJump == true && isDeparted == false) {
			if (kanonMan.transform.rotation.eulerAngles.y > 1f) {
				kanonMan.transform.rotation = Quaternion.Lerp (kanonMan.transform.rotation, Quaternion.Euler (0, 0, 0), 0.4f * Time.timeScale);
			} else {
				kanonMan.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				kanonMan.GetComponent<Animator> ().SetBool ("isTimeToJump", true);
				isReadyToJump = false;
			}
		} 

		// Leap before jump
		bool isNextHoppe = kanonMan.GetComponent<Animator> ().GetNextAnimatorStateInfo(0).IsName("Hop");
		bool isAnimatingz = kanonMan.GetComponent<Animator>().IsInTransition(0);
		if(isNextHoppe && isAnimatingz == true){
			kanonMan.transform.root.gameObject.GetComponent<Animation>().Play("KanonManJump");
			isCountingDownForJump = true;
		}

		// Jump before shooting
		if (isCountingDownForJump == true) 
		{
			// First Jump before Shooting
			if(jumpCountdown > 0) jumpCountdown -= Time.deltaTime;
			if(isAnimatingz == false && jumpCountdown <= 0 && isDeparted == false)//&& jumpCountdown == 0)
			{
				print (jumpRotation);
				kanonMan.transform.root.rotation = Quaternion.Lerp (kanonMan.transform.root.rotation, Quaternion.Euler (jumpRotation, 0, 0), jumpRotationSpeed * (Time.deltaTime*30));
				if (isJumpingAnimationActivated == false){
					kanonMan.transform.root.gameObject.GetComponent<Animation>().Play("KanonManLeapBeforeJump");
					isJumpingAnimationActivated = true;

					// Flag animation for dingle og spjætte ben :D
					kanonMan.GetComponent<Animator> ().SetBool ("isFlying", true);

					// Activate gaming camera
					isGameOn = true;
				}
			}

			// Last Jump before Shooting
			if(jumpShootCountdown > 0) jumpShootCountdown -= Time.deltaTime;
			if (jumpShootCountdown <= 0)
			{
				if (isDeparted == false)
				{
					isDeparted = true;
					kanonMan.GetComponent<KanonMan>().allowShooting = true;
					kanonMan.GetComponent<KanonMan>().Shoot(jumpRotation);

					// Show GUI
					GameObject.Find("GUI").GetComponent<Animation>().Play("GUI");

					// Stop Lunte in burning down + remove it.
					PFX_LunteDots.enableEmission = false;
					PFX_LunteStripes.enableEmission = false;
					kanonManLunte.SetActive(false);
				}
			}
		}
	}

	public void ShowDustParticles (ContactPoint contactPoint)
	{
		if (contactPoint.point != null) 
		{
			// Place Particle Effect where it hits the ground
			GameObject gObj = Instantiate(PfxHittingGround);
			gObj.transform.position = contactPoint.point;

			// Play Hitting Sand Sound
			int soundNum = Random.Range (0,2);
			if(soundNum == 0) 		GetComponent<SoundsTriggered>().playSound4(true, 1);
			else if (soundNum == 1)	GetComponent<SoundsTriggered>().playSound5(true, 1);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		ShowDustParticles (col.contacts[0]);
	}
}
