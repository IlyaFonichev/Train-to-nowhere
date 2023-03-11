using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptRoom", menuName = "Gameplay/New ScriptRoom")]
public class ScriptebleRoom : ScriptableObject
{
    [SerializeField] private int level;

    [SerializeField] private GameObject spawnPointChest;
    [SerializeField] private GameObject spawnPointBoss;
    [SerializeField] private GameObject[] spawnPointsOfMobs;
    [SerializeField] private GameObject[] spawnPointsOfDecorations;

    private List<Transform> spawnPoints;

    [SerializeField]
    private ScriptType type;
    private enum ScriptType
    {
        Empty,
        Start,
        Chest,
        Mobs,
        Boss
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}