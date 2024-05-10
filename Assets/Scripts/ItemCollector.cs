using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    private TextMeshProUGUI cherriesText;

    [SerializeField] private AudioSource pickUpSound;

    [SerializeField] private Canvas popUp;

    private DataManager dataManager;

    public void Awake()
    {
        GameObject textObject = GameObject.Find("Cherries Text");
        GameObject dataManagerObject = GameObject.Find("Datamanger");

        if (textObject != null)
        {
            cherriesText = textObject.GetComponent<TextMeshProUGUI>();
        }

        dataManager = GameObject.FindObjectOfType<DataManager>();

        if (dataManagerObject != null)
        {
            Debug.Log("Datamanager not found in the secne.");
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            pickUpSound.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Coins: " + cherries;
            Time.timeScale = 0;
            popUp.gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            dataManager.AddToPersistentList(cherries);
            dataManager.MergeSortPersistentList();
        }
    }
}
