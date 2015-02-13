using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public float speed = 2.0f;
	public float parentSpeed = 0.0f;
	
	public float lifetime = 3.0f;
	
	public GameObject hullHitEffect;

	public Transform sourceObject;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
					
	}

	void FixedUpdate()
	{
		transform.Translate( 0, 0, speed + parentSpeed );
	}

	void OnCollisionEnter(Collision collision) {
		if( !collision.transform.gameObject.CompareTag("Projectile") && sourceObject != null && collision.transform.root != sourceObject ){

			if( hullHitEffect )
			{
				OnProjectileCollision( collision.contacts[0].point );
			}

			//Debug.Log(this.name + " collided with " + collision.transform.name);
			collision.gameObject.SendMessageUpwards("OnProjectileHit", collision.transform, SendMessageOptions.DontRequireReceiver );

			Destroy(gameObject);
		}
    }
	
	void OnProjectileCollision( Vector3 position )
	{
		GameObject hitEffect = Instantiate( hullHitEffect, position, transform.rotation ) as GameObject;
		Destroy (hitEffect, hullHitEffect.particleSystem.startLifetime*2);
	}
	
	public void SetParentSpeed( float value )
	{
		parentSpeed = value;
	}
}
