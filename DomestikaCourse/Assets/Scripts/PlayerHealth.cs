using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 5;
    private int health;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        health = health - amount;
        //visual feedback
        StartCoroutine("VisualFeedback");
        //game over condition
        if (health <= 0)
        {
            health = 0;
        }
        Debug.Log("Player got damage, current health: " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        //Max Health
        if (health > totalHealth)
        {
            health = totalHealth;
        }
        //Debug.Log("Player got health, current health: " + health);

    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
    }
}
