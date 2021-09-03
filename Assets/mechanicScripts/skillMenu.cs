using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillMenu : MonoBehaviour
{
    public void skill1Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(1);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill1Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void skill2Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(2);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill2Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void skill3Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(3);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill3Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void skill4Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(4);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill4Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void skill5Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(5);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill5Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
    public void skill6Button()
    {
        if (Player.skillPoints > 0)
        {
            Player.skillPoints--;
            Player.skillCodes.Add(6);
            GameObject.Find("availableSkillPoints").GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();
            GameObject button = GameObject.Find("skill6Button");
            button.GetComponentInChildren<Text>().text = "unlocked";
            button.GetComponent<Image>().color = Color.green;
            button.GetComponent<Button>().enabled = false;
        }
    }
}
