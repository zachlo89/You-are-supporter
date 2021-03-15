using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/ListOfCharacters")]
public class ListOfHeroes : ScriptableObject
{
    public List<ScriptableCharacter> heroesList = new List<ScriptableCharacter>();
}
