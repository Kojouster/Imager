using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracker : MonoBehaviour
{
    // creating a an array of prefabs that will be awailable at run time
    [SerializeField]
    private GameObject[] placeblePrefabs;

    // this will call spawnned prefabs
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    // creating AR tracker
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        //getting and storing reference to the tracked image manager of type:
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // looping through prefab gameobjects
        foreach (GameObject prefab in placeblePrefabs)
        {
            //Instantiating the current loop prefab and setting the appropiarate scale and rotation
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            //making sure that the name is correct for serching it later
            newPrefab.name = prefab.name;
            // adding to spawned prefab dictionary
            spawnedPrefabs.Add(prefab.name, newPrefab);

        }
    }
    // Bind and UnBind images in the trackedImageManager
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageSwaped;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageSwaped;
    }

    //functionality based on which image is being tracked, removed or updated
    private void ImageSwaped(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // each time the image is added another function gets called
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // each time the image is added another function gets called
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // each time the image is added another function gets called
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }

    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        // temporarly storing the name of desired picture
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        // getting prefab by the name
        GameObject prefab = spawnedPrefabs[name];
        // to see the prefab to the linked image
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            // When looking at the new Image the current one will be hidden
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }

    }
}
