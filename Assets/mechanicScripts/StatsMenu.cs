using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    void Update()
    {
        GameObject.Find("LevelStatus").GetComponent<Text>().text = "Level: " + (Player.currentLevel + 1).ToString();
        GameObject.Find("TotalHealthStatus").GetComponent<Text>().text = "Total health: " + Player.totalHealth.ToString();
        GameObject.Find("AttackRangeStatus").GetComponent<Text>().text = "Attack range: " + Player.lowAttack.ToString() + "-" + Player.highAttack.ToString();
        GameObject.Find("CurrentEXPStatus").GetComponent<Text>().text = "Current EXP: " + Player.currentExp.ToString();
        GameObject.Find("EXPToNextLevelStatus").GetComponent<Text>().text = "EXP to next level: " + Player.expToNextLevel.ToString();
    }
}
