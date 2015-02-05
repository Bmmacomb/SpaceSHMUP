﻿using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	static public Hero   S;
	public float gameRestartDelay = 2f;

	public float speed = 30;
	public float rollMult = -45;
	public float pitchMult = 30;

	// status info
	[SerializeField]
	private float _shieldLevel = 1;

	public bool _____________________________;

	public Bounds bounds;

	void Awake(){
		S = this;
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// pull in info from Input class;
		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");
		// change pos based on axies 
		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;
		bounds.center = transform.position;

		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero) {
			pos -= off;
			transform.position = pos;
		}

		// add rotation to make it feel more dynamic
		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);
	}



	public GameObject lastTriggerGo = null;

	void OnTriggerEnter(Collider other){
		GameObject go = Utils.FindTaggedParent (other.gameObject);
		if (go != null) {
			if (go == lastTriggerGo){
				return;
			}
			lastTriggerGo = go;

			if (go.tag == "Enemy"){
				shieldLevel--;
				Destroy(go);
			}else{
				print("triggered: " + go.name);
			}
		//	print ("triggered: 	" + go.name);
		} else {
			print("Triggered: " +other.gameObject.name);
		}
	}
	public float shieldLevel{
		get{
			return(_shieldLevel);
		}
		set{
			_shieldLevel = Mathf.Min (value, 4);
			if (value < 0){
				Destroy(this.gameObject);
				Main.S.DelayedRestart(gameRestartDelay);
			}
		}
	}
}
