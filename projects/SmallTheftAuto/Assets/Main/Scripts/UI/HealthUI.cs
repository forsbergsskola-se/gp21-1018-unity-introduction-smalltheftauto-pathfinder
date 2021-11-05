using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;

    float health, maxHealth = 100;
    float lerpSpeed;

    public delegate void ThePlayerDiesEvent();

    public static event ThePlayerDiesEvent OnThePlayerDies;


    private void PlayerDies()
    {
        if (OnThePlayerDies != null)
        {
            OnThePlayerDies();
        }

        StartCoroutine(DelayFillHealth());
    }

    IEnumerator DelayFillHealth()
    {
        yield return new WaitForSeconds(3);
        health = maxHealth;
    }
    
    private void Start()
    {
        PainVolumeScript_ML.PainEvent += Damage;
        health = maxHealth;
    }

    private void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        lerpSpeed = 3f * Time.deltaTime;
        healthText.text = "Health: " + health + "%";
        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoint)
    {
        if(health> 0)
        {
            health -= damagePoint;
        }

        if (health == 0)
        {
            PlayerDies();
        }
    }

  
    
    public void Heal(float healPoint)
    {
        if(health < maxHealth)
        {
            health += healPoint;
        }
    }
}
