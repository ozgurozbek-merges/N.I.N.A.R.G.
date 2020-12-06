using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text CurrentHealth_Text;
    public Text MaxHealth_Text;

    private float CurrentHealth;
    public float MaxHealth;
    public bool Damagable;
    public void Hit(float Damage)
    {
        if (Damagable)
        {
            CurrentHealth-=Damage;
        }
    }

    //UI gelince bunu taşı :()
    private void Awake() {
        Damagable=true;
        CurrentHealth=MaxHealth;
        CurrentHealth_Text.text=CurrentHealth.ToString("0");
        MaxHealth_Text.text=MaxHealth.ToString("0");

    }
    private void Update() 
    {
        CurrentHealth_Text.text=CurrentHealth.ToString();
    }

}
