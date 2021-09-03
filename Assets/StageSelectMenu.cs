using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Finder
{
    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}

public class StageSelectMenu : MonoBehaviour
{
    GameObject availableSkillPoints;
    GameObject skill1Button;
    GameObject skill2Button;
    GameObject skill3Button;
    GameObject skill4Button;
    GameObject skill5Button;
    GameObject skill6Button;
    GameObject playerSkillDice;
    Vector3 cameraPos;


    public void Start()
    {
        GameObject P = GameObject.Find("Canvas");
        availableSkillPoints = Finder.FindObject(P, "availableSkillPoints");
        skill1Button = Finder.FindObject(P, "skill1Button");
        skill2Button = Finder.FindObject(P, "skill2Button");
        skill3Button = Finder.FindObject(P, "skill3Button");
        skill4Button = Finder.FindObject(P, "skill4Button");
        skill5Button = Finder.FindObject(P, "skill5Button");
        skill6Button = Finder.FindObject(P, "skill6Button");
        playerSkillDice = Finder.FindObject(P, "playerSkillDice");
        cameraPos = GameObject.Find("Main Camera").transform.position;
    }

    public void setSkillMenu()
    {
        availableSkillPoints.GetComponent<Text>().text = "Available Skill Points: " + Player.skillPoints.ToString();

        if (Player.skillCodes.Contains(1))
        {
            skill1Button.GetComponentInChildren<Text>().text = "unlocked";
            skill1Button.GetComponent<Image>().color = Color.green;
            skill1Button.GetComponent<Button>().enabled = false;
        }
        if (Player.skillCodes.Contains(2))
        {
            skill2Button.GetComponentInChildren<Text>().text = "unlocked";
            skill2Button.GetComponent<Image>().color = Color.green;
            skill2Button.GetComponent<Button>().enabled = false;
        }
        if (Player.skillCodes.Contains(3))
        {
            skill3Button.GetComponentInChildren<Text>().text = "unlocked";
            skill3Button.GetComponent<Image>().color = Color.green;
            skill3Button.GetComponent<Button>().enabled = false;
        }
        if (Player.skillCodes.Contains(4))
        {
            skill4Button.GetComponentInChildren<Text>().text = "unlocked";
            skill4Button.GetComponent<Image>().color = Color.green;
            skill4Button.GetComponent<Button>().enabled = false;
        }
        if (Player.skillCodes.Contains(5))
        {
            skill5Button.GetComponentInChildren<Text>().text = "unlocked";
            skill5Button.GetComponent<Image>().color = Color.green;
            skill5Button.GetComponent<Button>().enabled = false;
        }
        if (Player.skillCodes.Contains(6))
        {
            skill6Button.GetComponentInChildren<Text>().text = "unlocked";
            skill6Button.GetComponent<Image>().color = Color.green;
            skill6Button.GetComponent<Button>().enabled = false;
        }

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Skill dice the player currently has:");

        for (int i = 0; i < Player.skillDice.Count; ++i)
        {
            string str = "Dice " + (i + 1).ToString() + ": " + Player.skillDice[i].getLowRange().ToString() + "-" + Player.skillDice[i].getHighRange().ToString();
            sb.AppendLine("Dice " + (i + 1).ToString() + ": " + Player.skillDice[i].getLowRange().ToString() + "-" + Player.skillDice[i].getHighRange().ToString());
        }

        playerSkillDice.GetComponent<Text>().text = sb.ToString();
    }

    public void playEasyPyramid()
    {
        SceneManager.LoadScene("Easy8x8Level");
    }

    public void playHardPyramid()
    {
        SceneManager.LoadScene("Hard12x12Level");
    }

    public void returnToMenus()
    {
        SceneManager.LoadScene("Menus");
    }

    public void moveCamera()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(20, 0, 0);
    }

    public void moveCameraToOrigin()
    {
        GameObject.Find("Main Camera").transform.position = cameraPos;
    }
}
