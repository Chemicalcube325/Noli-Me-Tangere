using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizButton : MonoBehaviour
{

    [SerializeField] private Canvas popUp;
    [SerializeField] private GameObject player;


    public Health health;

    public void CorrectAnswer()
    {
        Debug.Log("Correct Answer: Button Clicked");
        Time.timeScale = 1;
        popUp.gameObject.SetActive(false);

    }
    public void WrongAnswer()
    {
        Debug.Log("Wrong Answer: Button Clicked");
        Time.timeScale = 1;
        popUp.gameObject.SetActive(false);

        health.TakeDamage(1);
    }

    void Start()
    {
        health = player.GetComponent<Health>();

        if (health == null)
        {
            Debug.LogError("Health Component not found");
        }
    }

    void Update()
    {
        
    }
}
