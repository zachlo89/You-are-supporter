using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopRewardReward
{
    gold,
    normalChest,
    epicChest,
    rubins
}

[CreateAssetMenu(menuName = "ScriptableObject/ShopReward")]
public class ShopRewardScriptable : ScriptableObject
{
    public int cost;
    public ShopRewardReward reward;
    public int count;
}
