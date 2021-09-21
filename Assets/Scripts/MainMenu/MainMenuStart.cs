using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainMenuStart : MonoBehaviour
{
	[SerializeField] private List<GameObject> planets;

	public static Action<int> OnChooseLvl;
	
	public int lastLvl = -1;
	public int currentLvl = -1;

	private void Start()
	{
		OnChooseLvl += SetLevelOnButtonClick;
	}

	private void SetLevelOnButtonClick(int lvl)
	{
		currentLvl = lvl;
		if (lastLvl >= 0)
			planets[lastLvl].GetComponent<BezierMover>().TransformToStartPosition();
		planets[lvl].GetComponent<BezierMover>().TransformToCamera();
		lastLvl = lvl;
	}

	public void StartLevelButtonClick()
	{
		SceneController.instance.LoadLevel(currentLvl);
	}

	private void OnDestroy()
	{
		OnChooseLvl -= SetLevelOnButtonClick;
	}
}
