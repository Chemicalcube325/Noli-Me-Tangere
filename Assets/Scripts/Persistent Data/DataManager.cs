using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEditor.Experimental.GraphView;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public List<int> persistentList = new List<int>();
    public TextMeshProUGUI outputText; // Reference to TMP Text UI element

    public static DataManager Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<DataManager>();

                if (instance == null)
                {
                    instance = new GameObject("DataManager").AddComponent<DataManager>();
                }

                DontDestroyOnLoad(instance.gameObject);
                

            }

            return instance;
        }
    }

    private void Awake()
    {
        // Check's if theres already an instance in the scene
        if (instance != null && instance != this)
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad (gameObject);

        GameObject highScoresObject = GameObject.FindWithTag("HighScores");

        if (highScoresObject != null)
        {
            Debug.Log("Found HighScores Object");
            outputText = highScoresObject.GetComponent<TextMeshProUGUI>();


            if (outputText != null)
            {
                Debug.Log("Found TextmeshProUGUI component");
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the HighScores GameObject.");
            }
        }

        else 
        {
            Debug.LogError("HighScores GameObject not found.");
        }

        // Ensure the outputText is assigned during Awake
        if (outputText == null)
        {
            Debug.LogWarning("outputText not assigned during Awake. Please check the Unity Editor.");
        }

     
    }

    // Merge-sort method for sorting the persistent list.
    public void MergeSortPersistentList()
    {
        if (Instance != null)
        {
            Debug.Log("MergeSortPersistentList");
            instance.persistentList = MergeSort(instance.persistentList);
            UpdateOutputText(); // Call the method to update the TMP Text
        }
        else 
        {
            Debug.LogError("DataManger Instance is Null in MergeSortPersistentLIst");
        }

    }

    private List<int> MergeSort(List<int> unsortedList)
    {
        Debug.Log("MergeSort");
        if (unsortedList.Count <= 1)
        {
            return unsortedList;
        }

        // Split the list into two halves.
        int middle = unsortedList.Count / 2;
        List<int> left = new List<int>(unsortedList.GetRange(0, middle));
        List<int> right = new List<int>(unsortedList.GetRange(middle, unsortedList.Count - middle));

        // Recursively sort each half.
        left = MergeSort(left);
        right = MergeSort(right);

        // Merge the sorted halves.
        return Merge(left, right);
    }

    private List<int> Merge(List<int> left, List<int> right)
    {
        List<int> result = new List<int>();
        int leftIndex = 0;
        int rightIndex = 0;

        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            // Compare elements and add the smaller one to the result.
            if (left[leftIndex] > right[rightIndex])
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }
            else
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }
        }

        // Add remaining elements from both lists (if any).
        while (leftIndex < left.Count)
        {
            result.Add(left[leftIndex]);
            leftIndex++;
        }

        while (rightIndex < right.Count)
        {
            result.Add(right[rightIndex]);
            rightIndex++;
        }

        return result;
    }

    // Method to update TMP Text with the sorted list
    private void UpdateOutputText()
    {
        Debug.Log("UpdateOutputText");
        if (outputText != null)
        { 
            // Check if TextMeshPro component is available
            if (outputText.GetComponent<TextMeshProUGUI>() == null)
            {
                Debug.LogError("TextMeshPro component not found!");
                return;
            }

        Debug.Log("Updating TMP Text with sorted list.");
        string output = string.Join("\n", persistentList.ConvertAll(i => i.ToString()));
        outputText.text = output;
        }
        else
        {
        Debug.LogError("outputText is not assigned!");
        }
    }

    // Method to add an element to the persistentList
    public void AddToPersistentList(int value)
    {
        Debug.Log("AddToPersistentList");
        persistentList.Add(value);
        UpdateOutputText(); // Call the method to update the TMP Text
    }

}