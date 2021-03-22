using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Team")]
public class Team : ScriptableObject
{
    public List<ScriptableCharacter> heroesList = new List<ScriptableCharacter>();
    public int maxTeamSize;
    public int avaliabeTeamSize;
    
    public void AddCharacter(ScriptableCharacter hero, int position)
    {
        if(position <= avaliabeTeamSize && position <= maxTeamSize)
        {
            heroesList[position] = hero;
        }
    }

    public void RemoveCharacter(int position)
    {
        heroesList.RemoveAt(position);
    }

}
