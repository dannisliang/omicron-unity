using UnityEngine;
using System.Collections;

public class BlasterScript : MonoBehaviour {

	public bool fire = false;


	public GameObject laserEmitter;

	public float laserSpeed = 5;

	public GameObject laserPrefab;
	public ParticleSystem flashSystem;

	public AudioClip[] blasterSounds;

	float shotDelayTimer = 0;
	public float shotDelay = 0.25f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if( fire && shotDelayTimer <= 0 )
		{
			flashSystem.Emit(1);

			GameObject laser = Instantiate( laserPrefab, laserEmitter.transform.position, laserEmitter.transform.rotation ) as GameObject;
			ProjectileScript projectileScript = laser.GetComponent<ProjectileScript>();
			projectileScript.sourceObject = transform;
			projectileScript.speed = laserSpeed;
			projectileScript.parentSpeed = 0;
			fire = false;

			int randomSound = Random.Range(0, blasterSounds.Length );
			AudioSource.PlayClipAtPoint(blasterSounds[randomSound], laserEmitter.transform.position);

			shotDelayTimer = shotDelay;
		}
		else if( shotDelayTimer > 0 )
		{
			shotDelayTimer -= Time.deltaTime;
		}
	}


	public void Fire()
	{
		fire = true;
	}
}
