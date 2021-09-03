using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Easy6x6LevelMechanics : MonoBehaviour
{
    Mechanics mechanics;

    private void Start()
    {
        List<Enemy> enemies;
        Player.row = 5;
        Player.col = 5;
        Player.playerModel = GameObject.Find("MainCharacter");
        Level level = new Level(4, 6, 6);
        enemies = new List<Enemy>();

        GameObject enemy1 = GameObject.Find("EasyEnemy");
        GameObject enemy2 = GameObject.Find("EasyEnemy (1)");
        GameObject enemy3 = GameObject.Find("EasyEnemy (2)");
        GameObject enemy4 = GameObject.Find("EasyEnemy (3)");

        enemies.Add(new Enemy(1, 4, 15, 2, enemy1, 0, 0));
        enemies.Add(new Enemy(1, 4, 15, 2, enemy2, 0, 1));
        enemies.Add(new Enemy(1, 4, 15, 2, enemy3, 0, 2));
        enemies.Add(new Enemy(1, 4, 15, 2, enemy4, 0, 3));

        level.getSpace(0, 0).setEnemy(enemies[0]);
        level.getSpace(0, 1).setEnemy(enemies[1]);
        level.getSpace(0, 2).setEnemy(enemies[2]);
        level.getSpace(0, 3).setEnemy(enemies[3]);

        level.getSpace(5, 5).setPlayer();

        GameObject.Find("skillRollStatus").GetComponent<Text>().text = "Skill Rolls:";
        GameObject.Find("turnStatus").GetComponent<Text>().text = "Turn: Player";
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions:";
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();

        mechanics = new Mechanics(level, GameObject.Find("UpButton"), GameObject.Find("LeftButton"), GameObject.Find("RightButton"), GameObject.Find("DownButton"), GameObject.Find("StatsButton"), GameObject.Find("SkillsButton"), GameObject.Find("OptionsButton"), GameObject.Find("RollButton"), GameObject.Find("EndTurnButton"), GameObject.Find("MainCharacter").GetComponent<Animator>(), 6, 6, enemies, "Easy4x4Level", this);

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
