using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    static Player()
    {
        skillDice.Add(new die(1, 6));

        //skillCodes.Add(1);
        //skillCodes.Add(2);
        //skillCodes.Add(3);
        //skillCodes.Add(4);
        //skillCodes.Add(5);
        //skillCodes.Add(6);
    }

    public static int attack()
    {
        int damage = Random.Range(lowAttack, highAttack + 1);

        if (doubleDamageFlag)
        {
            damage = damage * 2;
            doubleDamageFlag = false;
        }
        if (qaudDamageFlag)
        {
            damage = damage * 4;
            qaudDamageFlag = false;
        }

        return damage;
    }

    public static int actionRoll()
    {
        return Random.Range(1, 6 + 1);
    }

    public static bool takeDamage(int amount)
    {
        if (zeroDamageFlag)
        {
            zeroDamageFlag = false;
            return false;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            return true;
        }

        return false;
    }

    public static bool addExp(int amount)
    {
        currentExp += amount;

        if (currentExp >= expToNextLevel)
        {
            currentLevel++;
            currentExp = currentExp - expToNextLevel;
            expToNextLevel += 5;
            totalHealth += 5;
            skillPoints++;
            currentHealth = totalHealth;
            lowAttack = lowAttack + 1;
            highAttack = highAttack + 1;

            return true;
        }

        return false;
    }

    public static int getCurrentLevel()
    {
        return currentLevel;
    }
    public static int getRow()
    {
        return row;
    }
    public static void setRow(int r)
    {
        row = r;
    }
    public static int getCol()
    {
        return col;
    }
    public static void setCol(int c)
    {
        col = c;
    }

    public static GameObject getPlayerModel()
    {
        return playerModel;
    }

    public static int getCurrentHealth()
    {
        return currentHealth;
    }

    public static int getTotalHealth()
    {
        return totalHealth;
    }

    public static List<int> rollSkillDie()
    {
        List<int> rolls = new List<int>();

        for (int i = 0; i < skillDice.Count; ++i)
        {
            rolls.Add(skillDice[i].roll());
        }

        return rolls;
    }

    public static int lowAttack = 5;
    public static int highAttack = 10;
    public static int totalHealth = 50;
    public static int currentHealth = 50;
    public static int currentLevel = 0;
    public static int expToNextLevel = 5;
    public static int currentExp = 0;
    public static GameObject playerModel = null;
    public static int row = 0;
    public static int col = 0;
    public static List<die> skillDice = new List<die>();
    public static List<int> skillCodes = new List<int>();
    public static int skillPoints = 0;
    public static bool zeroDamageFlag = false;
    public static bool doubleDamageFlag = false;
    public static bool qaudDamageFlag = false;
}
