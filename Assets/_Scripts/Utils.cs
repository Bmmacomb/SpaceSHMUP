using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils : MonoBehaviour {


	//=============================================Bounds Functions===================================\\ 

	// creates bounds that encap the two bounds passed in.

	public static Bounds BoundsUnion(Bounds b0 , Bounds b1){
		// if the size of one of the bounds is Vector3.zero ignore it;
		if (b0.size == Vector3.zero && b1.size != Vector3.zero) {
			return(b1);		
		} else if (b0.size != Vector3.zero && b1.size == Vector3.zero) {
			return(b0);
		} else if (b0.size == Vector3.zero && b1.size == Vector3.zero) {
			return(b0);
		}
		// stretch b0 to include b1.min & b1 max
		b0.Encapsulate (b1.min);
		b0.Encapsulate (b1.max);
		return (b0);

	}


	public static Bounds CombineBoundsOfChildren(GameObject go){
		Bounds b = new Bounds (Vector3.zero, Vector3.zero);
		if (go.renderer != null) {
			b = BoundsUnion(b,go.renderer.bounds);
		}
		if (go.collider != null) {
			b = BoundsUnion(b,go.collider.bounds);
		}
		foreach (Transform t in go.transform) {
			b = BoundsUnion(b,CombineBoundsOfChildren(t.gameObject));		
		}
		return b;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
