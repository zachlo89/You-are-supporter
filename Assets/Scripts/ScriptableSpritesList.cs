using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SpriteList")]
public class ScriptableSpritesList : ScriptableObject
{
    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();
}
