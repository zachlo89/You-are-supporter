using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] public List<Level> levelsList = new List<Level>();
    public Sprite image;
}
