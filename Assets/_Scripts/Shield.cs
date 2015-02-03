using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float rotationsPerSecond = .1f;
	public bool __________________________;
	public int levelShown = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//  read cur shield level
		int curLevel = Mathf.FloorToInt (Hero.S.shieldLevel);
		// if diffrent
		if (levelShown != curLevel) {
			levelShown = curLevel;
			Material mat = this.renderer.material;
			//adj offset
			mat.mainTextureOffset = new Vector2(.2f*levelShown,0);
		}
		// rotate the shield every second
		float rZ = (rotationsPerSecond * Time.time * 360) % 360f;
		transform.rotation = Quaternion.Euler (0, 0, rZ);
	
	}
}
