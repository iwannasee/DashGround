using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour {
	//TODO remove this bool after
	private static bool isActivated = false;

	private static Dictionary<string,string> SecretMapDict_0 = new Dictionary<string,string>();
	private static Dictionary<string,string> SecretMapDict_1 = new Dictionary<string, string>();

	public static readonly Dictionary<string,string>[] wholeGameSecretMaps = new Dictionary<string, string>[MapDictionary.WORLD_SUM_NUMBER]{
		SecretMapDict_0,
		SecretMapDict_1
	};

	// Use this for initialization
	void Awake () {
		if(!isActivated){
			SecretMapDict_0.Add ("11111", MapDictionary.w1_SC_map_vHole);
			SecretMapDict_0.Add ("22222", MapDictionary.w1_SC_map_sSho);

			SecretMapDict_1.Add ("33333", MapDictionary.w2_SC_map_cGrv);
			SecretMapDict_1.Add ("44444", MapDictionary.w2_SC_map_nReamth);

			isActivated = true;
		}
	}

}
