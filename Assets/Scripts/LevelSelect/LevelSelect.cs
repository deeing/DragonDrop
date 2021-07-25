using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Supported list of names of levels. Name must be same as build name")]
    private string[] levelList;
    [SerializeField]
    [Tooltip("Prefab for a level entry in the list of level selection.")]
    private GameObject levelEntryPrefab;

    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;

        for(int i=0; i < levelList.Length; i++)
        {
            string levelName = levelList[i];
            GameObject levelEntryObj = Instantiate(levelEntryPrefab, thisTransform);
            LevelEntry levelEntry = levelEntryObj.GetComponent<LevelEntry>();
            levelEntry.SetName(levelName);
            levelEntry.SetupButtonCallback();
        }
    }
}
