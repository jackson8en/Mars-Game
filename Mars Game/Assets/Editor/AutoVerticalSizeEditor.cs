using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoVerticalSize))]
public class AutoVerticalSizeEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		if(GUILayout.Button ("Re-calc Size")){
			((AutoVerticalSize)target).AdjustSize ();
		}
	}

}
