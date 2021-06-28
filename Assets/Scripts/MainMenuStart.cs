using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainMenuStart : MonoBehaviour
{
	[SerializeField]
	private Transform starsTransform;

	[SerializeField] private VisualEffect sun;

	[SerializeField] private List<GameObject> planets;

	private void Start()
	{
		// StartCoroutine(StarsCoroutine());
		// sun.Stop();
	}
	
	private float speedFactor = 0.5f;
	
	public int lastLvl = -1;
	public void SetLevelOnButtonClick(int lvl)
	{
		if (lastLvl >= 0)
			planets[lastLvl].GetComponent<BezierTest>().TransformToStartPosition();
		planets[lvl].GetComponent<BezierTest>().TransformToCamera();
		lastLvl = lvl;
	}
	
}
