using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	public Vector2 rotMinMax = new Vector2(15,90);
	public Vector2 driftMinMax = new Vector2(.25f,2);
	public float lifeTime = 6f;
	public float fadeTime = 4f;
	public bool _____________;
	public WeaponType type;
	public GameObject cube;
	public TextMesh letter;
	public Vector3 rotPerSecond;
	public float birthTime;



	void Awake(){
		cube = transform.Find ("Cube").gameObject;

		letter = GetComponent<TextMesh> ();
		Vector3 vel = Random.onUnitSphere;
		vel.z = 0;
		vel.Normalize();
		vel *= Random.Range (driftMinMax.x, driftMinMax.y);
		rigidbody.velocity = vel;
		transform.rotation = Quaternion.identity;
		rotPerSecond = new Vector3 (Random.Range (rotMinMax.x, rotMinMax.y),
		                           Random.Range (rotMinMax.x, rotMinMax.y),
		                           Random.Range (rotMinMax.x, rotMinMax.y));
		InvokeRepeating ("CheckOffScreen", 2f, 2f);
		birthTime = Time.time;


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
