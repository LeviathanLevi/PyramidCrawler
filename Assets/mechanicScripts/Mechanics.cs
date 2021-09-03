using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Enemy
{
    public Enemy(int lowAtk, int highAtk, int hp, int exp, GameObject enemyMesh, int r, int c)
    {
        lowAttack = lowAtk;
        highAttack = highAtk;
        health = hp;
        expDrop = exp;
        isDead = false;
        enemyModel = enemyMesh;
        row = r;
        col = c;
    }

    public int attack()
    {
        return Random.Range(lowAttack, highAttack + 1);
    }

    public int actionRoll()
    {
        return Random.Range(1, 6);
    }

    public bool takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            enemyModel.SetActive(false);
            return true;
        }

        return false;
    }

    public int getExpDrop()
    {
        return expDrop;
    }

    public bool getIsDead()
    {
        return isDead;
    }
    public int getRow()
    {
        return row;
    }
    public void setRow(int r)
    {
        row = r;
    }
    public int getCol()
    {
        return col;
    }
    public void setCol(int c)
    {
        col = c;
    }

    public GameObject getModel()
    {
        return enemyModel;
    }

    private int lowAttack;
    private int highAttack;
    private int health;
    private int expDrop;
    private bool isDead;
    private GameObject enemyModel;
    private int row;
    private int col;
}
public class Space
{
    public Space(int r, int c, bool occupied, Enemy enemy, bool occPlayer)
    {
        row = r;
        col = c;
        isOccupied = occupied;
        occupyingEnemy = enemy;
        occupyingPlayer = occPlayer;
    }

    public Space(int r, int c)
    {
        row = r;
        col = c;
        isOccupied = false;
        occupyingEnemy = null;
        occupyingPlayer = false;
    }

    public void clearSpace()
    {
        isOccupied = false;
        occupyingEnemy = null;
        occupyingPlayer = false;
    }

    public void setPlayer()
    {
        isOccupied = true;
        occupyingPlayer = true;
        occupyingEnemy = null;
    }
    public void setEnemy(Enemy e)
    {
        isOccupied = true;
        occupyingPlayer = false;
        occupyingEnemy = e;
    }

    public bool getIsOccupied()
    {
        return isOccupied;
    }
    public bool getIsOccupiedByPlayer()
    {
        return occupyingPlayer;
    }

    public Enemy getEnemyOccupant()
    {
        return occupyingEnemy;
    }

    private int row;
    private int col;
    private bool isOccupied;
    private Enemy occupyingEnemy;
    private bool occupyingPlayer;
}

public class Level
{
    public Level(int numOfEnemies, int rows, int cols)
    {
        numberOfEnemies = numOfEnemies;
        rowSize = rows;
        colSize = cols;
        boardSquares = new Space[rows, cols];

        for (int i = 0; i < boardSquares.GetLength(0); i++)
            for (int j = 0; j < boardSquares.GetLength(1); j++)
                boardSquares[i, j] = new Space(i, j);
    }

    public Space getSpace(int row, int col)
    {
        return boardSquares[row, col];
    }

    public bool removeEnemy()
    {
        numberOfEnemies--;

        if (numberOfEnemies == 0)
        {
            return true;
        }

        return false;
    }

    private int numberOfEnemies;
    private int rowSize;
    private int colSize;
    private Space[,] boardSquares;
}

public class die
{
    public die(int low, int high)
    {
        lowRange = low;
        highRange = high;
    }

    public void setLowRange(int val)
    {
        lowRange = val;
    }

    public int getLowRange()
    {
        return lowRange;
    }

    public void setHighRange(int val)
    {
        highRange = val;
    }

    public int getHighRange()
    {
        return highRange;
    }

    public int roll()
    {
        return Random.Range(lowRange, highRange+1);
    }

    private int lowRange;
    private int highRange;
}

public class Mechanics
{
    public Mechanics(Level l, GameObject upB, GameObject lB, GameObject rB, GameObject dB, GameObject sB, GameObject skB, GameObject oB, GameObject roB, GameObject eB, Animator pAnimator, int rSize, int cSize, List<Enemy> e, string nScene, MonoBehaviour m)
    {
        actions = 0;
        level = l;
        upButton = upB;
        leftButton = lB;
        rightButton = rB;
        downButton = dB;
        statButton = sB;
        skillsButton = skB;
        optionsButton = oB;
        rollButton = roB;
        endTurnButton = eB;
        playerAnimator = pAnimator;
        rowSize = rSize;
        colSize = cSize;
        enemies = e;
        nextScene = nScene;
        mono = m;
    }

    public void moveCharacterUp()
    {
        if (actions > 0)
        {
            if (Player.getRow() != 0 && !level.getSpace(Player.getRow() - 1, Player.getCol()).getIsOccupied())
            {
                playerAnimator.SetTrigger("move");
                level.getSpace(Player.getRow(), Player.getCol()).clearSpace();
                level.getSpace(Player.getRow() - 1, Player.getCol()).setPlayer();
                Player.setRow(Player.getRow() - 1);
                Player.getPlayerModel().transform.Translate(new Vector3(0, 0, 1));
                updateMovementButtons();
                EventSystem.current.SetSelectedGameObject(null);
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
            else if (level.getSpace(Player.getRow() - 1, Player.getCol()).getIsOccupied())
            {
                attackEnemy(level.getSpace(Player.getRow() - 1, Player.getCol()));
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
        }
    }

    public void moveCharacterLeft()
    {
        if (actions > 0)
        {
            if (Player.getCol() != 0 && !level.getSpace(Player.getRow(), Player.getCol() - 1).getIsOccupied())
            {
                playerAnimator.SetTrigger("move");
                level.getSpace(Player.getRow(), Player.getCol()).clearSpace();
                level.getSpace(Player.getRow(), Player.getCol() - 1).setPlayer();
                Player.setCol(Player.getCol() - 1);
                Player.getPlayerModel().transform.Translate(new Vector3(-1, 0, 0));
                updateMovementButtons();
                EventSystem.current.SetSelectedGameObject(null);
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
            else if (level.getSpace(Player.getRow(), Player.getCol() - 1).getIsOccupied())
            {
                attackEnemy(level.getSpace(Player.getRow(), Player.getCol() - 1));
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
        }
    }
    public void moveCharacterRight()
    {
        if (actions > 0)
        {
            if (Player.getCol() != colSize - 1 && !level.getSpace(Player.getRow(), Player.getCol() + 1).getIsOccupied())
            {
                playerAnimator.SetTrigger("move");
                level.getSpace(Player.getRow(), Player.getCol()).clearSpace();
                level.getSpace(Player.getRow(), Player.getCol() + 1).setPlayer();
                Player.setCol(Player.getCol() + 1);
                Player.getPlayerModel().transform.Translate(new Vector3(1, 0, 0));
                updateMovementButtons();
                EventSystem.current.SetSelectedGameObject(null);
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
            else if (level.getSpace(Player.getRow(), Player.getCol() + 1).getIsOccupied())
            {
                attackEnemy(level.getSpace(Player.getRow(), Player.getCol() + 1));
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
        }
    }

    public void moveCharacterDown()
    {
        if (actions > 0)
        {
            if (Player.getRow() != rowSize - 1 && !level.getSpace(Player.getRow() + 1, Player.getCol()).getIsOccupied())
            {
                playerAnimator.SetTrigger("move");
                level.getSpace(Player.getRow(), Player.getCol()).clearSpace();
                level.getSpace(Player.getRow() + 1, Player.getCol()).setPlayer();
                Player.setRow(Player.getRow() + 1);
                Player.getPlayerModel().transform.Translate(new Vector3(0, 0, -1));
                updateMovementButtons();
                EventSystem.current.SetSelectedGameObject(null);
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
            else if (level.getSpace(Player.getRow() + 1, Player.getCol()).getIsOccupied())
            {
                attackEnemy(level.getSpace(Player.getRow() + 1, Player.getCol()));
                actions--;
                GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
            }
        }
    }

    public void attackEnemy(Space space)
    {
        playerAnimator.SetTrigger("attack");

        int damage = Player.attack();
        Enemy occupant = space.getEnemyOccupant();
        Animator occupantAnimator = occupant.getModel().GetComponent<Animator>();
        occupantAnimator.SetTrigger("getHit");
        bool isKilled = occupant.takeDamage(damage);

        if (isKilled)
        {
            if (Player.addExp(occupant.getExpDrop()))
            {
                Debug.Log("Level Up!");
                GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();
            }

            if (level.removeEnemy())
            {
                endLevel();
            }

            enemies.Remove(occupant);
            space.clearSpace();
        }

        updateMovementButtons();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void updateMovementButtons()
    {
        // up:
        if (Player.getRow() - 1 < 0)
        {
            upButton.SetActive(false);
        }
        else
        {
            upButton.SetActive(true);
            if (level.getSpace(Player.getRow() - 1, Player.getCol()).getIsOccupied())
            {
                ColorBlock colors = upButton.GetComponent<Button>().colors;
                colors.normalColor = Color.red;
                upButton.GetComponent<Button>().colors = colors;
            }
            else
            {
                ColorBlock colors = upButton.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                upButton.GetComponent<Button>().colors = colors;
            }
        }

        // left:
        if (Player.getCol() - 1 < 0)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
            if (level.getSpace(Player.getRow(), Player.getCol() - 1).getIsOccupied())
            {
                ColorBlock colors = leftButton.GetComponent<Button>().colors;
                colors.normalColor = Color.red;
                leftButton.GetComponent<Button>().colors = colors;
            }
            else
            {
                ColorBlock colors = leftButton.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                leftButton.GetComponent<Button>().colors = colors;
            }
        }

        // right:
        if (Player.getCol() + 1 > colSize - 1)
        {
            rightButton.SetActive(false);
        }
        else
        {
            rightButton.SetActive(true);
            if (level.getSpace(Player.getRow(), Player.getCol() + 1).getIsOccupied())
            {
                ColorBlock colors = rightButton.GetComponent<Button>().colors;
                colors.normalColor = Color.red;
                rightButton.GetComponent<Button>().colors = colors;
            }
            else
            {
                ColorBlock colors = rightButton.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                rightButton.GetComponent<Button>().colors = colors;
            }
        }

        // down:
        if (Player.getRow() + 1 > rowSize - 1)
        {
            downButton.SetActive(false);
        }
        else
        {
            downButton.SetActive(true);
            if (level.getSpace(Player.getRow() + 1, Player.getCol()).getIsOccupied())
            {
                ColorBlock colors = downButton.GetComponent<Button>().colors;
                colors.normalColor = Color.red;
                downButton.GetComponent<Button>().colors = colors;
            }
            else
            {
                ColorBlock colors = downButton.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                downButton.GetComponent<Button>().colors = colors;
            }
        }
    }   

    public void endTurn()
    {
        GameObject.Find("skillRollStatus").GetComponent<Text>().text = "Skill Rolls:";
        GameObject.Find("turnStatus").GetComponent<Text>().text = "Turn: Enemy";
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions:";
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();

        mono.StartCoroutine(enemyTurn());
    }

    IEnumerator enemyTurn()
    {
        disableAllButtons(true);
        
        //Enemy turns:
        for (int i = 0; i < enemies.Count; ++i)
        {
            yield return new WaitForSeconds(0.25f);
            int actions = enemies[i].actionRoll();

            while (actions > 0)
            {
                //move/attack:
                if (Player.getRow() > enemies[i].getRow())
                {
                    yield return new WaitForSeconds(0.5f);

                    if (level.getSpace(enemies[i].getRow() + 1, enemies[i].getCol()).getIsOccupiedByPlayer())
                    {
                        //attack
                        enemyAttack(enemies[i], level.getSpace(enemies[i].getRow() + 1, enemies[i].getCol()));
                    }
                    else if (!level.getSpace(enemies[i].getRow() + 1, enemies[i].getCol()).getIsOccupied())
                    {
                        //move
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol()).clearSpace();
                        level.getSpace(enemies[i].getRow() + 1, enemies[i].getCol()).setEnemy(enemies[i]);
                        enemies[i].setRow(enemies[i].getRow() + 1);
                        Animator enemyAnimator = enemies[i].getModel().GetComponent<Animator>();
                        enemyAnimator.SetTrigger("move");
                        enemies[i].getModel().transform.Translate(new Vector3(-1, 0, 0));

                    }
                }
                else if (Player.getRow() < enemies[i].getRow())
                {
                    yield return new WaitForSeconds(0.5f);

                    if (level.getSpace(enemies[i].getRow() - 1, enemies[i].getCol()).getIsOccupiedByPlayer())
                    {
                        //attack
                        enemyAttack(enemies[i], level.getSpace(enemies[i].getRow() - 1, enemies[i].getCol()));
                    }
                    else if (!level.getSpace(enemies[i].getRow() - 1, enemies[i].getCol()).getIsOccupied())
                    {
                        //move
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol()).clearSpace();
                        level.getSpace(enemies[i].getRow() - 1, enemies[i].getCol()).setEnemy(enemies[i]);
                        enemies[i].setRow(enemies[i].getRow() - 1);
                        Animator enemyAnimator = enemies[i].getModel().GetComponent<Animator>();
                        enemyAnimator.SetTrigger("move");
                        enemies[i].getModel().transform.Translate(new Vector3(1, 0, 0));

                    }
                }
                else if (Player.getCol() > enemies[i].getCol())
                {
                    yield return new WaitForSeconds(0.5f);

                    if (level.getSpace(enemies[i].getRow(), enemies[i].getCol() + 1).getIsOccupiedByPlayer())
                    {
                        //attack
                        enemyAttack(enemies[i], level.getSpace(enemies[i].getRow(), enemies[i].getCol() + 1));
                    }
                    else if (!level.getSpace(enemies[i].getRow(), enemies[i].getCol() + 1).getIsOccupied())
                    {
                        //move
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol()).clearSpace();
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol() + 1).setEnemy(enemies[i]);
                        enemies[i].setCol(enemies[i].getCol() + 1);
                        Animator enemyAnimator = enemies[i].getModel().GetComponent<Animator>();
                        enemyAnimator.SetTrigger("move");
                        enemies[i].getModel().transform.Translate(new Vector3(0, 0, -1));

                    }
                }
                else if (Player.getCol() < enemies[i].getCol())
                {
                    yield return new WaitForSeconds(0.5f);

                    if (level.getSpace(enemies[i].getRow(), enemies[i].getCol() - 1).getIsOccupiedByPlayer())
                    {
                        //attack
                        enemyAttack(enemies[i], level.getSpace(enemies[i].getRow(), enemies[i].getCol() - 1));
                    }
                    else if (!level.getSpace(enemies[i].getRow(), enemies[i].getCol() - 1).getIsOccupied())
                    {
                        //move
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol()).clearSpace();
                        level.getSpace(enemies[i].getRow(), enemies[i].getCol() - 1).setEnemy(enemies[i]);
                        enemies[i].setCol(enemies[i].getCol() - 1);
                        Animator enemyAnimator = enemies[i].getModel().GetComponent<Animator>();
                        enemyAnimator.SetTrigger("move");
                        enemies[i].getModel().transform.Translate(new Vector3(0, 0, 1));

                    }
                }


                actions--;
            }
        }

        yield return new WaitForSeconds(.5f);

        //Player skills:
        List<int> skillRolls = Player.rollSkillDie();

        if (Player.skillCodes.Contains(1))
        {
            bool skill1Flag = false;
            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 3)
                {
                    skill1Flag = true;
                }
            }
            if (skill1Flag)
            {
                Debug.Log("Skill 1 casted.");

                Player.currentHealth += System.Convert.ToInt32(Player.totalHealth * .2);
                if (Player.currentHealth > Player.totalHealth)
                {
                    Player.currentHealth = Player.totalHealth;
                }
                GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();
            }
        }

        if (Player.skillCodes.Contains(2))
        {
            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 8)
                {
                    Debug.Log("Skill 2 casted.");
                    Player.zeroDamageFlag = true;
                }
            }
        }

        if (Player.skillCodes.Contains(3))
        {
            int numberFourCount = 0;

            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 4)
                {
                    numberFourCount++;
                }
            }

            if (numberFourCount >= 2)
            {
                Debug.Log("Skill 3 casted.");
                Player.currentHealth = Player.totalHealth;
                GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();
            }
        }

        if (Player.skillCodes.Contains(4))
        {

            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 1)
                {
                    Player.doubleDamageFlag = true;
                    Debug.Log("Skill 4 casted.");
                }
            }
        }

        if (Player.skillCodes.Contains(5))
        {
            bool skillTrueFlag = false;

            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 7)
                {
                    skillTrueFlag = true;
                }
            }

            if (skillTrueFlag)
            {
                Debug.Log("Skill 5 casted.");
                for (int i = 0; i < enemies.Count; ++i)
                {
                    attackEnemy(level.getSpace(enemies[i].getRow(), enemies[i].getCol()));
                }
            }
        }

        if (Player.skillCodes.Contains(6))
        {
            int numberFiveCount = 0;

            for (int i = 0; i < skillRolls.Count; ++i)
            {
                if (skillRolls[i] == 5)
                {
                    numberFiveCount++;
                }
            }

            if (numberFiveCount >= 2)
            {
                Debug.Log("Skill 6 casted.");
                Player.qaudDamageFlag = true;
            }
        }

        disableAllButtons(false);
        var sb = new System.Text.StringBuilder();
        sb.Append("Rolls:");
        for (int i = 0; i < skillRolls.Count; ++i)
        {
            sb.Append(skillRolls[i].ToString() + " ");
        }
        GameObject.Find("skillRollStatus").GetComponent<Text>().text = sb.ToString();

        updateMovementButtons();
    }

    private void enemyAttack(Enemy e, Space s)
    {
        int damage = e.attack();
        Animator enemyAnimator = e.getModel().GetComponent<Animator>();
        enemyAnimator.SetTrigger("attack");
        if (Player.takeDamage(damage))
        {
            playerDied();
        }
        playerAnimator.SetTrigger("getHit");
        GameObject.Find("healthStatus").GetComponent<Text>().text = "Health: " + Player.getCurrentHealth().ToString() + "/" + Player.getTotalHealth().ToString();
    }

    public void disableAllButtons(bool disable)
    {
        if (disable)
        {
            upButton.SetActive(false);
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            downButton.SetActive(false);
            statButton.SetActive(false);
            skillsButton.SetActive(false);
            rollButton.SetActive(false);
            endTurnButton.SetActive(false);
            optionsButton.SetActive(false);
        }
        else
        {
            upButton.SetActive(true);
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            downButton.SetActive(true);
            statButton.SetActive(true);
            skillsButton.SetActive(true);
            rollButton.SetActive(true);
            endTurnButton.SetActive(true);
            optionsButton.SetActive(true);
        }
    }


    public void playerDied()
    {
        Player.currentExp = 0;
        Player.currentHealth = Player.totalHealth;
        SceneManager.LoadScene("Menus");
    }

    public void endLevel()
    {
        Player.currentHealth = Player.totalHealth;
        SceneManager.LoadScene(nextScene);
    }

    public void rollForActions()
    {
        actions = Player.actionRoll();
        GameObject.Find("actionsStatus").GetComponent<Text>().text = "Actions: " + actions.ToString();
        rollButton.SetActive(false);
    }

    private int actions;
    private Level level;
    private GameObject upButton;
    private GameObject leftButton;
    private GameObject rightButton;
    private GameObject downButton;
    private GameObject statButton;
    private GameObject skillsButton;
    private GameObject optionsButton;
    private GameObject rollButton;
    private GameObject endTurnButton;
    private Animator playerAnimator;
    private int rowSize;
    private int colSize;
    private List<Enemy> enemies;
    private string nextScene;
    private MonoBehaviour mono;
}