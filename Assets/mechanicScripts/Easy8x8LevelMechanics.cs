using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Easy8x8LevelMechanics : MonoBehaviour
{
    Mechanics mechanics;

    private void Start()
    {
        List<Enemy> enemies;
        Player.row = 7;
        Player.col = 4;
        Player.playerModel = GameObject.Find("MainCharacter");
        Level level = new Level(3, 8, 8);
        enemies = new List<Enemy>();

        GameObject enemy1 = GameObject.Find("EasyEnemy");
        GameObject enemy2 = GameObject.Find("EasyEnemy (1)");
        GameObject enemy3 = GameObject.Find("EasyEnemy (2)");
        enemies.Add(new Enemy(1, 3, 10, 2, enemy1, 0, 2));
        enemies.Add(new Enemy(1, 3, 10, 2, enemy2, 0, 3));
        enemies.Add(new Enemy(1, 3, 10, 2, enemy3, 0, 4));

        level.getSpace(0, 2).setEnemy(enemies[0]);
        level.getSpace(0, 3).setEnemy(enemies[1]);
        level.getSpace(0, 4).setEnemy(enemies[2]);

        level.getSpace(7, 4).setPlayer();

        GameObject.Find("skillRollStatus").GetComponent<Text>().text = "Skill Rolls:";
        GameObject.Find("turnStatus").GetComponent<Text>().text = "Turn: Player";
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions:";
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();

        mechanics = new Mechanics(level, GameObject.Find("UpButton"), GameObject.Find("LeftButton"), GameObject.Find("RightButton"), GameObject.Find("DownButton"), GameObject.Find("StatsButton"), GameObject.Find("SkillsButton"), GameObject.Find("OptionsButton"), GameObject.Find("RollButton"), GameObject.Find("EndTurnButton"), GameObject.Find("MainCharacter").GetComponent<Animator>(), 8, 8, enemies, "Easy6x6Level", this);

        mechanics.updateMovementButtons();
    }

    public void moveCharacterUp()
    {
        mechanics.moveCharacterUp();
    }

    public void moveCharacterLeft()
    {
        mechanics.moveCharacterLeft();
    }

    public void moveCharacterRight()
    {
        mechanics.moveCharacterRight();
    }

    public void moveCharacterDown()
    {
        mechanics.moveCharacterDown();
    }

    public void endTurn()
    {
        mechanics.endTurn();
    }

    public void rollForActions()
    {
        mechanics.rollForActions();
    }
}