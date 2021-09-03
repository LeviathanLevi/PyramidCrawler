using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rewardMenu : MonoBehaviour
{
    public void addNewDice()
    {
        Player.skillDice.Add(new die(1, 6));
        SceneManager.LoadScene("Menus");
    }

    public void upgradeDice()
    {
        Player.skillDice[0].setHighRange(Player.skillDice[0].getHighRange() + 1);
        Player.skillDice[0].setLowRange(Player.skillDice[0].getLowRange() + 1);
        SceneManager.LoadScene("Menus");
    }
}
