using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hard6x6LevelMechanics : MonoBehaviour
{
    Mechanics mechanics;

    private void Start()
    {
        List<Enemy> enemies;
        Player.row = 4;
        Player.col = 4;
        Player.playerModel = GameObject.Find("MainCharacter");
        Level level = new Level(1, 6, 6);
        enemies = new List<Enemy>();

        GameObject enemy1 = GameObject.Find("EasyBoss");

        enemies.Add(new Enemy(1, 12, 60, 50, enemy1, 1, 1));

        level.getSpace(1, 1).setEnemy(enemies[0]);

        level.getSpace(4, 4).setPlayer();

        GameObject.Find("skillRollStatus").GetComponent<Text>().text = "Skill Rolls:";
        GameObject.Find("turnStatus").GetComponent<Text>().text = "Turn: Player";
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions:";
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();

        mechanics = new Mechanics(level, GameObject.Find("UpButton"), GameObject.Find("LeftButton"), GameObject.Find("RightButton"), GameObject.Find("DownButton"), GameObject.Find("StatsButton"), GameObject.Find("SkillsButton"), GameObject.Find("OptionsButton"), GameObject.Find("RollButton"), GameObject.Find("EndTurnButton"), GameObject.Find("MainCharacter").GetComponent<Animator>(), 6, 6, enemies, "Reward Select", this);

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