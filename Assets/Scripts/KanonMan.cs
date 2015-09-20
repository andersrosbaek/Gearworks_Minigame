using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KanonMan : MonoBehaviour 
{
	float xRotationSpeed = 400f;
	float xSpeed = 2f;
	float jumpSpeed = 100f;
	float ShootPushback = 300;
	float ShootCooldownTemporaryDivider = 1;
	float ShootCooldown = 20;
	float ShootCooldownReset = 80;
	int shots = 4;
	int scoreTotal;
	int endGameCoundown = 40;
	public int scoreKegler;
	public bool isGrounded;
	public bool allowShooting = false;

	int currentWarmupExerciseNum = 0;
	int currentWarmupExerciseCountdownNum = 300;
	int currentWarmupExerciseCountdownResetNum = 300;

	public GameObject PFX_ShootingSmokeRadial;
	public GameObject PFX_ShootingSmokeAhead;
	public GameObject PFX_HittingGround;
	public ControlPanel SCRIPT_ControlPanel;
	public ScoreBoard SCRIPT_ScoreBoard;
	public Text TXT_Score;
	
	// Use this for initialization
	void Start () {
		// Temporary divider
		ShootCooldown = ShootCooldown / ShootCooldownTemporaryDivider;
		ShootCooldownReset = ShootCooldownReset / ShootCooldownTemporaryDivider;
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateAnimatorParameters ();
		UpdateWarmupExercise ();
		UpdateShootingDetection ();
		UpdateRotation ();
		UpdateScore ();
		UpdateEndingGame ();
	}

	void UpdateScore ()
	{
		if (allowShooting && isGrounded == false)
		{
			scoreTotal = Mathf.RoundToInt(transform.root.position.z) + scoreKegler;
			TXT_Score.text = scoreTotal + "";
		}
	}

	void UpdateAnimatorParameters ()
	{
		// Update Animation Parameters
		GetComponent<Animator>().SetFloat("xSpeed", transform.root.gameObject.GetComponent<Rigidbody>().velocity.x);
		GetComponent<Animator>().SetFloat("ySpeed", transform.root.gameObject.GetComponent<Rigidbody>().velocity.y);
		GetComponent<Animator>().SetBool("isGrounded", isGrounded);
		GetComponent<Animator>().SetInteger("currentWarmupExerciseNum", currentWarmupExerciseNum);
	}

	void UpdateWarmupExercise ()
	{
		if (allowShooting == false) {
			// Countdown to next warmup
			currentWarmupExerciseCountdownNum--;
			if (currentWarmupExerciseCountdownNum == 0) {
				currentWarmupExerciseCountdownNum 	= Random.Range(1, currentWarmupExerciseCountdownResetNum);
				currentWarmupExerciseNum 			= Random.Range(0,3);
			}
		}
	}

	void UpdateShootingDetection ()
	{
		// Lower Cooldown
		if (allowShooting) {
			if (ShootCooldown > 0)
				ShootCooldown -= Time.deltaTime * 40; 
			
			// Shoot :D
			if (Input.GetKeyUp(KeyCode.Space) && ShootCooldown <= 0) 
			{
				// Reset Cooldown
				ShootCooldown = ShootCooldownReset;
				
				// Move KanonMan when Shooting
				float xRotation = Mathf.Abs(transform.root.rotation.x) * 360 + 180;
				Shoot(xRotation);			
			}
		}
	}

	void UpdateRotation ()
	{
		if (allowShooting && isGrounded == false) 
		{
			transform.root.Rotate(xRotationSpeed * Time.deltaTime,0,0);
			//transform.root.rotation = Quaternion.Euler(new Vector3(transform.root.rotation.eulerAngles.x + xRotationSpeed, 0,0));
		}
	}

	public void Shoot(float jumpRotation)
	{
		// Shoot if shots left and hasn't hit ground
		if (shots > 0 && isGrounded == false) 
		{
			// Set rotation
			//transform.root.rotation = Quaternion.Euler(new Vector3(jumpRotation, 0, 0));

			// Take away shot-tries
			shots--;

			// Play Shot Sound
			GetComponent<SoundsTriggered> ().playSound1 ();
			
			// Pfx when Shooting
			PFX_ShootingSmokeAhead.GetComponent<ParticleSystem>().enableEmission = true;
			PFX_ShootingSmokeRadial.GetComponent<ParticleSystem>().enableEmission = true;
			PFX_ShootingSmokeAhead.GetComponent<ParticleSystem>().Play ();
			PFX_ShootingSmokeRadial.GetComponent<ParticleSystem>().Play ();
			PFX_ShootingSmokeAhead.GetComponent<ParticleSystem>().Emit (200);
			PFX_ShootingSmokeRadial.GetComponent<ParticleSystem>().Emit (200);
			
			
			// Push the KanonMan the right direction
			float velocity = 1 + ( Mathf.Abs(transform.root.gameObject.GetComponent<Rigidbody> ().velocity.y)) / 50;
			transform.root.gameObject.GetComponent<Rigidbody>().AddForce(-transform.root.up * ShootPushback * velocity);
			transform.root.gameObject.GetComponent<Rigidbody>().useGravity = true;
		}
	}

	// By some reason regular OnCollisionEnter won't work. 
	// Therefore I call CollisionEnter from a GameObject wrapping this GameObject.
	public void CollisionEnter(Collision col) 
	{
		if (col.collider.name == "Ground")
		{
			if (isGrounded == false)
			{
				isGrounded = true;

				// Show Pfx Hitting Ground
				ShowDustParticles (col.contacts[0]);
			}
		}
	}

	public void ShowDustParticles (ContactPoint contactPoint)
	{
		if (contactPoint.point != null) 
		{
			// Place Particle Effect where it hits the ground
			GameObject gObj = Instantiate(PFX_HittingGround);
			gObj.transform.position = contactPoint.point;
			
			// Play Hitting Sand Sound
			int soundNum = Random.Range (0,2);
			if(soundNum == 0) 		GetComponent<SoundsTriggered>().playSound2(true, 1);
			else if (soundNum == 1)	GetComponent<SoundsTriggered>().playSound3(true, 1);
		}
	}

	void UpdateEndingGame ()
	{
		if (isGrounded) 
		{
			if (transform.root.gameObject.GetComponent<Rigidbody> ().velocity.z < 0.4f)
			{
				if (endGameCoundown == 0){
					GameObject.Find("GUI").GetComponent<Animation>().Play("Retry");
					SCRIPT_ScoreBoard.SubmitScore(scoreTotal);
				}
				if (endGameCoundown >= 0)
					endGameCoundown--;
			}
		}
	}
}
