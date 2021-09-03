using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hard12x12LevelMechanics : MonoBehaviour
{
    Mechanics mechanics;

    private void Start()
    {
        List<Enemy> enemies;
        Player.row = 11;
        Player.col = 11;
        Player.playerModel = GameObject.Find("MainCharacter");
        Level level = new Level(6, 12, 12);
        enemies = new List<Enemy>();

        GameObject enemy1 = GameObject.Find("HardEnemy");
        GameObject enemy2 = GameObject.Find("HardEnemy (1)");
        GameObject enemy3 = GameObject.Find("HardEnemy (2)");
        GameObject enemy4 = GameObject.Find("HardEnemy (3)");
        GameObject enemy5 = GameObject.Find("HardEnemy (4)");
        GameObject enemy6 = GameObject.Find("HardEnemy (5)");

        enemies.Add(new Enemy(1, 6, 15, 5, enemy1, 0, 0));
        enemies.Add(new Enemy(1, 6, 15, 5, enemy2, 0, 1));
        enemies.Add(new Enemy(1, 6, 15, 5, enemy3, 0, 2));
        enemies.Add(new Enemy(1, 6, 15, 5, enemy4, 0, 3));
        enemies.Add(new Enemy(1, 6, 15, 5, enemy5, 0, 4));
        enemies.Add(new Enemy(1, 6, 15, 5, enemy6, 0, 5));


        level.getSpace(0, 0).setEnemy(enemies[0]);
        level.getSpace(0, 1).setEnemy(enemies[1]);
        level.getSpace(0, 2).setEnemy(enemies[2]);
        level.getSpace(0, 3).setEnemy(enemies[3]);
        level.getSpace(0, 4).setEnemy(enemies[4]);
        level.getSpace(0, 5).setEnemy(enemies[5]);

        level.getSpace(11, 11).setPlayer();

        GameObject.Find("skillRollStatus").GetComponent<Text>().text = "Skill Rolls:";
        GameObject.Find("turnStatus").GetComponent<Text>().text = "Turn: Player";
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions:";
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();

        mechanics = new Mechanics(level, GameObject.Find("UpButton"), GameObject.Find("LeftButton"), GameObject.Find("RightButton"), GameObject.Find("DownButton"), GameObject.Find("StatsButton"), GameObject.Find("SkillsButton"), GameObject.Find("OptionsButton"), GameObject.Find("RollButton"), GameObject.Find("EndTurnButton"), GameObject.Find("MainCharacter").GetComponent<Animator>(), 12, 12, enemies, "Hard10x10Level", this);

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