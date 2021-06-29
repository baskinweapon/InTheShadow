using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainMenuStart : MonoBehaviour
{
	[SerializeField] private List<GameObject> planets;
	
	public int lastLvl = -1;
	public void SetLevelOnButtonClick(int lvl)
	{
		if (lastLvl >= 0)
			planets[lastLvl].GetComponent<BezierTest>().TransformToStartPosition();
		planets[lvl].GetComponent<BezierTest>().TransformToCamera();
		lastLvl = lvl;
	}
	
}
