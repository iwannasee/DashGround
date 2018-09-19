using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour {
	//---------------------------------------------------------------
	public const string char_pippo = "Pippo";
	public const string char_johnny = "Johnny"; 
	public const string char_mathial = "Mathial"; 
	public const string char_kolav = "Kolav"; 

	public static List <string> charNameList = new List<string>(new string[]{
		char_pippo,
		char_johnny,
		char_mathial,
		char_kolav
	});
	//---------------------------------------------------------------
	public const string veh_oven = "Oven";


	public static List <string> vehicleList = new List<string>(new string[]{
		veh_oven
	});

	//---------------------------------------------------------------




}
