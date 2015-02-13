using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
	public GameObject player;

	public GameObject laserPrefab;
	public ParticleSystem flashSystem;
	public GameObject laserEmitter;

	float shotDelayTimer = 0;
	public float shotDelay = 0.25f;
	public float laserSpeed = 5;
	public bool fire;

	public AudioClip[] blasterSounds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");

		Ray ray = new Ray( transform.position, player.transform.position-transform.position );
		RaycastHit hit;
		
		// Get the first collider that was hit by the ray
		bool hitSomething = Physics.Raycast(ray, out hit, 100);
		if( hitSomething )
		{
			Debug.DrawLine(ray.origin, hit.point); // Draws a line in the editor
			if( hit.transform.CompareTag("Player") )
			{
				transform.LookAt(hit.point);
				Fire ();
			}
		}
	}

	void Fire()
	{
		if( shotDelayTimer <= 0 )
		{
			flashSystem.Emit(1);
			transform.Rotate( Random.Range(-5,5), Random.Range(-5,5), Random.Range(-5,5) );

			GameObject laser = Instantiate( laserPrefab, laserEmitter.transform.position, laserEmitter.transform.rotation ) as GameObject;
			ProjectileScript projectileScript = laser.GetComponent<ProjectileScript>();
			projectileScript.speed = laserSpeed;
			projectileScript.parentSpeed = 0;
			projectileScript.sourceObject = transform;
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
}
