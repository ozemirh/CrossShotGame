using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelButtonCreation : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject parentObject;
    private int levelCount;

    private const int maxObjectsInRow = 3;
    private const float startX = -300f;
    private const float startY = 750f;
    private const float rowOffsetY = -300f;
    private const float offsetBetweenObjects = 300f;

    public List<GameObject> createdObjects = new List<GameObject>();


    private void Start()
    {
        levelCount = SceneManager.sceneCountInBuildSettings - 1;
        CreateObjectsInPattern(levelCount);
        for (int i = 0; i < levelCount; i++)
        {
            TMP_Text text = createdObjects[i].transform.GetChild(0).GetComponent<TMP_Text>();
            int value = i + 1;
            text.text = value.ToString();
        }
    }
    private void CreateObjectsInPattern(int totalObjects)
    {
        int count = 0;

        for (int i = 0; count < totalObjects; i++)
        {
            int remainingObjects = totalObjects - count;
            int objectsInRow = Mathf.Min(remainingObjects, maxObjectsInRow);
            CreateRowOfObjects(i, i + objectsInRow, objectsInRow);
            count += objectsInRow;
        }
    }
    private void CreateRowOfObjects(int rowNumber, int index, int objectsInRow)
    {
        float currentY = startY + rowOffsetY * rowNumber;
        for (int i = 0; i < objectsInRow; i++)
        {
            float currentX = startX + offsetBetweenObjects * i;
            Vector3 position = new Vector3(currentX, currentY, 0);
            GameObject newObject = Instantiate(levelObject, position, Quaternion.identity);
            newObject.transform.SetParent(parentObject.transform, false);          
            createdObjects.Add(newObject);
        }
    }
}
