using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	static public Main S;
	static public Dictionary<WeaponType,WeaponDefinition> W_DEFS;


	public GameObject[] PrefabEnemies;
	public float enemySpawnPerSecond = 0.5f;
	public float enemySpawnPadding = 1.5f;
	public WeaponDefinition[] weaponDefinitions;

	public GameObject prefabPowerUp;
	public WeaponType[] powerUpFrequency = new WeaponType[]{
											WeaponType.blaster,WeaponType.laser,
											WeaponType.pulse,WeaponType.shield,
											WeaponType.spread,WeaponType.torpedo,
											WeaponType.Ze_Bomb,WeaponType.blaster,
											WeaponType.HAX
	};

	public bool _____________;



	public WeaponType[]  activeWeaponTypes;
	public float enemySpawnRate;


	void Awake(){
		S = this;
		Utils.SetCameraBounds (this.camera);
		enemySpawnRate = 1f / enemySpawnPerSecond;
		Invoke ("SpawnEnemy", enemySpawnRate);

		W_DEFS = new Dictionary<WeaponType, WeaponDefinition> ();
		foreach (WeaponDefinition def in weaponDefinitions) {
			W_DEFS[def.type] = def;	
		}

	}


	public void SpawnEnemy(){
		int ndx = Random.Range (0, PrefabEnemies.Length);
		GameObject go = Instantiate (PrefabEnemies [ndx]) as GameObject;
		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x + enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - enemySpawnPadding;
		pos.x = Random.Range (xMin, xMax);
		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
		go.transform.position = pos;

		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	public void DelayedRestart(float delay){
		Invoke ("Restart", delay);
	}
	public void Restart(){
		Application.LoadLevel ("Scene0");
	}


	static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
		//make sure key exists
		//attemptin to retreve a nonexistant key would throw an error
		if (W_DEFS.ContainsKey (wt)) {
			return(W_DEFS[wt]);	
		}
		// return weapon.none if not found
		return(new WeaponDefinition ());
	}

	// Use this for initialization
	void Start () {
		activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
		for (int i = 0; i < weaponDefinitions.Length; i++) {
			activeWeaponTypes[i]=weaponDefinitions[i].type;	
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ShipDestroyed(Enemy e){
		if (Random.value <= e.powerUpDropChance) {
			int ndx = Random.Range(0,powerUpFrequency.Length);
			WeaponType puType = powerUpFrequency[ndx];

			GameObject go  = Instantiate(prefabPowerUp)as GameObject;
			PowerUp pu = go.GetComponent<PowerUp>();
			pu.SetType(puType);
			pu.transform.position = e.transform.position;
		}

	}
}
