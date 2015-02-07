﻿using UnityEngine;
using System.Collections;

public enum WeaponType{
	none,
	blaster,
	spread,
	laser,
	torpedo,
	pulse,
	Ze_Bomb,
	HAX,


	shield
}
[System.Serializable]
public class WeaponDefinition{
	public WeaponType type = WeaponType.none;
	public string letter;
	public Color color = Color.white;
	public GameObject projectilePrefab;
	public Color projectileColor = Color.white;
	public float damageOnHit =0 ;
	public float continuousDamage = 0;
	public float delayBetweenShots = 0;
	public float velocity =20;

}



public class Weapon : MonoBehaviour {
	static public Transform 	PROJECTILE_ANCHOR;

	public bool ________________;
	[SerializeField]
	private WeaponType _type = WeaponType.blaster;
	public WeaponDefinition def;
	public GameObject collar;
	public float lastShot;

	void Awake(){
		collar = transform.Find ("Collar").gameObject;
	}
	// Use this for initialization
	void Start () {

		SetType(_type);
		if (PROJECTILE_ANCHOR == null) {
			GameObject go = new GameObject("_Projectile_Anchor");
			PROJECTILE_ANCHOR = go.transform;
		}
		GameObject parentGO = transform.parent.gameObject;
		if (parentGO.tag == "Hero") {
			Hero.S.fireDelegate += Fire;	
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public WeaponType type{
		get {	return(_type);	}
		set	{	SetType(value);	}

	}

	public void SetType(WeaponType wt){
		_type = wt;
		if (type == WeaponType.none) {
			this.gameObject.SetActive(false);
			return;
		}else{
			this.gameObject.SetActive(true);
		}
		def = Main.GetWeaponDefinition (_type);
		collar.renderer.material.color = def.color;
		lastShot = 0;
	}

	public void Fire(){
		if (!gameObject.activeInHierarchy) {
			return;

		}
		if (Time.time - lastShot < def.delayBetweenShots) {
			return;	
		}
		Projectile p;
		switch (type) {
		case WeaponType.blaster:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			break;

		case WeaponType.torpedo:

			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.03f,.5f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.03f,.5f,0)*def.velocity;
			break;

		case WeaponType.laser:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			break;

		case WeaponType.spread:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.2f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.2f,.9f,0)*def.velocity;
			break;

		case WeaponType.pulse:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.1f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.1f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.2f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.2f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.3f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.3f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.4f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.4f,.9f,0)*def.velocity;

			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.5f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.5f,.9f,0)*def.velocity;
		   


			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.05f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.05f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.15f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.15f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.25f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.25f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.35f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.35f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.45f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.45f,.9f,0)*def.velocity;
			
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.55f,.9f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.55f,.9f,0)*def.velocity;
			break;


		case WeaponType.Ze_Bomb:

			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			break;


		case WeaponType.HAX:
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(-.6f,.5f,0)*def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3(.6f,.5f,0)*def.velocity;
			break;
			

		}
	}
	public Projectile MakeProjectile(){
		GameObject go = Instantiate (def.projectilePrefab) as GameObject;
		if (transform.parent.gameObject.tag == "Hero") {
			go.tag = "ProjectileHero";
			go.layer = LayerMask.NameToLayer("ProjectileHero");
		}else {
			go.tag = "ProjectileEnemy";
			go.layer = LayerMask.NameToLayer("ProjectileEnemy");
		}
		go.transform.position = collar.transform.position;
		go.transform.parent = PROJECTILE_ANCHOR;
		Projectile p = go.GetComponent<Projectile> ();
		p.type = type;
		lastShot = Time.time;
		return(p);
	}
}
