using System;
using UnityEngine;
using System.Collections;
using System.Reflection;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;    // ���� �ð�
    private Animator animator;
    private TopDownAnimationController animationController;
    private CharacterStatHandler statsHandler;
    private float timeSinceLastChange = float.MaxValue; // ������ ������ �ް� ����� �ð�
    private bool isAttacked = false;
    private bool isInvincible = false;

    private BoxCollider2D boxcollider;
    private Rigidbody2D rb;

    public event Action<float, float> OnHealthChanged;

    // ü���� ������ �� �� �ൿ���� �����ϰ� ���� ����
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action<float> OnInvincibilityStart;
    public event Action OnInvincibilityEnd;
    public event Action OnGameOver;

    public float CurrentHealth { get; set; }

    // get�� ������ ��ó�� ������Ƽ�� ����ϴ� ��
    // ������ ���ϼ� & ���ռ� ����
    public float MaxHealth => statsHandler.CurrentStat.maxHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();    
        boxcollider = GetComponent<BoxCollider2D>();
        statsHandler = GetComponent<CharacterStatHandler>();
        animationController = GetComponent<TopDownAnimationController>();

        OnGameOver += GameManager.Instance.GameOver;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        if (gameObject.CompareTag("Player"))
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void Update()
    {
        // ���ݹ��� �����̰� ���� �ð��̸�
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            // ���� �ð��� ������
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();   // ���� ���� ���� �̺�Ʈ ȣ��
                isAttacked = false; // �ǰ� ������ ���·� ����
            }
        }
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); 
        OnHeal?.Invoke(); 

        Debug.Log($"hp�� {healAmount}��ŭ ȸ���Ǿ����ϴ�. ���� hp: {CurrentHealth}");
    }

    public bool ChangeHealth(float change)
    {
        // ���� ���°ų� ���� �ð��� ������ �ʾҴٸ� ü�� ���� ��ȿȭ
        if (isInvincible || timeSinceLastChange < healthChangeDelay)
            return false;

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        // [�ּڰ��� 0, �ִ��� MaxHealth�� �ϴ� ����]
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        // CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        // CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth; �� ���ƿ�!

        // �÷��̾� ü�� UI ����
        if (gameObject.CompareTag("Player"))
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        // �÷��̾� ü���� 0 ���ϸ� ���� ���� ȣ��
        if (gameObject.CompareTag("Player") && CurrentHealth <= 0f)
        {
            Debug.Log("�÷��̾� ü��0");
            OnGameOver?.Invoke(); 
            return true;
        }
        // �� ü���� 0 ���ϸ� ��� �̺�Ʈ ȣ�� 
        if (CurrentHealth <= 0f && gameObject.CompareTag("Enemy"))
       {
            
            CallDeath();
            return true;
        }
        // �������� ����� �� �̺�Ʈ ȣ��
        if (change >= 0)
        {
            OnHeal?.Invoke();
            Debug.Log("����������");
        }
        // �������� ������ ������ �̺�Ʈ ȣ��
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;  // �ǰ� ���·� ����
        }
        return true;
    }

    private void CallDeath()
    {
        float score = statsHandler.CurrentStat.attackSO.speed * statsHandler.CurrentStat.attackSO.power;
        DataManager.Instance.IncrementScore(score * 0.5f);
        StartCoroutine(DeathSequence());
    }

    private void OnEnable()
    {
        timeSinceLastChange = float.MaxValue;
        rb.isKinematic = false;
    }

    IEnumerator DeathSequence()
    {
        animator.SetTrigger("IsDie");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Death"));
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Death") ? "����" : "�ƴ�");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        OnDeath?.Invoke();
    }


    public void Invincibility(float duration)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            OnInvincibilityStart?.Invoke(duration);

            Invoke(nameof(DisableInvincibility), duration);
        }
    }
    private void DisableInvincibility()
    {
        isInvincible = false;
        OnInvincibilityEnd?.Invoke();
    }
}