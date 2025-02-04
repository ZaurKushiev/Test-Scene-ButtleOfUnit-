using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Unit playerUnit;
    public Unit enemyUnit;

    private bool playerTurn = true;

    private void Start()
    {
        StartBattle();
    }

    private void StartBattle()
    {
        playerUnit.currentHealth = playerUnit.maxHealth;
        enemyUnit.currentHealth = enemyUnit.maxHealth;
        playerTurn = true;
    }

    private void Update()
    {
        CheckBattleStatus();

        if (playerTurn)
        {
            // Логика выбора способности игроком
            // Например, если игрок нажал кнопку атаки:
            playerUnit.Attack(enemyUnit);
            playerTurn = false; // Передаем ход противнику
        }
        else
        {
            EnemyTurn();
            playerTurn = true; // Передаем ход игроку
        }
    }

    private void EnemyTurn()
    {
        // Простой ИИ, выбирающий случайное действие
        int action = Random.Range(0, 5); // 5 - количество способностей

        switch (action)
        {
            case 0:
                enemyUnit.Attack(playerUnit);
                break;
            case 1:
                enemyUnit.UseBarrier();
                break;
            case 2:
                enemyUnit.UseRegeneration();
                break;
            case 3:
                enemyUnit.UseFireball(playerUnit);
                break;
            case 4:
                enemyUnit.Cleanse();
                break;
        }
    }

    private void CheckBattleStatus()
    {
        if (playerUnit.currentHealth <= 0 || enemyUnit.currentHealth <= 0)
        {
            RestartBattle();
        }
    }

    private void RestartBattle()
    {
        StartBattle(); // Перезапускаем бой
    }
}
