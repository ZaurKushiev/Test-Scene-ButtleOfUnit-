using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHealth = 20;
    public int currentHealth;
    public float attackCooldown = 0;
    public float barrierCooldown = 0;
    public float regenerationCooldown = 0;
    public float fireballCooldown = 0;
    public float cleanseCooldown = 0;

    private bool isBarrierActive = false;
    private int barrierDuration = 0;
    private bool isBurning = false;
    private int burnDuration = 0;
    private int regenDuration = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Обновление кулдаунов
        UpdateCooldowns();
        // Обработка эффектов
        HandleEffects();
    }

    private void UpdateCooldowns()
    {
        if (attackCooldown > 0) attackCooldown--;
        if (barrierCooldown > 0) barrierCooldown--;
        if (regenerationCooldown > 0) regenerationCooldown--;
        if (fireballCooldown > 0) fireballCooldown--;
        if (cleanseCooldown > 0) cleanseCooldown--;

        if (barrierDuration > 0) barrierDuration--;
        if (burnDuration > 0) burnDuration--;
        if (regenDuration > 0) regenDuration--;
    }

    private void HandleEffects()
    {
        // Обработка регенерации
        if (regenDuration > 0)
        {
            currentHealth += 2; // Восстанавливаем здоровье
            if (currentHealth > maxHealth) currentHealth = maxHealth; // Ограничиваем здоровье
        }

        // Обработка горения
        if (isBurning && burnDuration > 0)
        {
            currentHealth -= 1; // Наносим урон от горения
        }
    }

    public void Attack(Unit target)
    {
        if (attackCooldown <= 0)
        {
            int damage = isBarrierActive ? Mathf.Max(0, 8 - 5) : 8; // Учитываем барьер
            target.TakeDamage(damage);
            attackCooldown = 1; // Установить кулдаун
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void UseBarrier()
    {
        if (barrierCooldown <= 0)
        {
            isBarrierActive = true;
            barrierDuration = 2; // Длительность барьера
            barrierCooldown = 4; // Перезарядка
        }
    }

    public void UseRegeneration()
    {
        if (regenerationCooldown <= 0)
        {
            regenDuration = 3; // Длительность регенерации
            regenerationCooldown = 5; // Перезарядка
        }
    }

    public void UseFireball(Unit target)
    {
        if (fireballCooldown <= 0)
        {
            target.TakeDamage(5);
            isBurning = true;
            burnDuration = 5; // Длительность горения
            fireballCooldown = 6; // Перезарядка
        }
    }

    public void Cleanse()
    {
        if (cleanseCooldown <= 0)
        {
            isBurning = false; // Снимаем эффект горения
            cleanseCooldown = 5; // Перезарядка
        }
    }
}

