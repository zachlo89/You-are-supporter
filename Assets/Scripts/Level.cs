using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    public List<ScriptableCharacter> enemiesList = new List<ScriptableCharacter>();
    public int itemsRarityDrop;
    public Sprite background;
    public bool isPassed;
    [Range(0,3)]
    public int stars;
    public bool isAvaliable;
    public bool hasGoldLevel;
}
