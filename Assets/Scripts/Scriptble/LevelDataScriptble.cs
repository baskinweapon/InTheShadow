using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class LevelDataScriptble : ScriptableObject
{
    public string nameLevel;
    public bool isOpen;
    public float time;
    public int dificult;
    public int score;

}
