using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDraw : MonoBehaviour {


	void OnDrawGizmos(){
	Gizmos.color = Color.cyan;
	Gizmos.DrawWireSphere (transform.position, 0.3f);
	}
}
