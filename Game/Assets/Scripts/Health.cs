using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text CurrentHealth_Text;
    public Text MaxHealth_Text;

    private float CurrentHealth;
    public float MaxHealth;
    public bool Damageable;
    public void Hit(float Damage)
    {
        if (Damageable)
        {
            CurrentHealth -= Damage;
        }
    }

    public void ChangeDamageable(bool Value)
    {
        Damageable = Value;
    }

    //UI gelince bunu taşı :()
    private void Awake()
    {
        Damageable = true;
        CurrentHealth = MaxHealth;
        CurrentHealth_Text.text = CurrentHealth.ToString("0");
        MaxHealth_Text.text = MaxHealth.ToString("0");

    }
    private void Update()
    {
        CurrentHealth_Text.text = CurrentHealth.ToString();
    }

}
