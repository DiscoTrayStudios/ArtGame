using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    public Canvas inventory;
    public Canvas pauseMenu;
    public GameObject player;
    public Camera cam;
    public Terrain terrain;
    public GameObject rockprefab;
    private Collider pcollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void buttonPress(string name)
    {
        cam.orthographic = true;
        if (name.Equals("luncheon"))
        {
            cam.transform.position = new Vector3(1.4f, 12f, -22f);
            cam.transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else if (name.Equals("lilypads"))
        {
            cam.transform.position = new Vector3(2.16f, 10.84f, -20.54f);
        }
        else if (name.Equals("and i was there"))
        {
            cam.transform.position = new Vector3(26.75f, 45.48f, -20.54f);
        }
        //else if (name.Equals("ice pies"))
        //{
        //    Debug.Log("Not implemented yet!");
        //    cam.transform.position = new Vector3(12.92f, 15.16f, 31.19f);
        //    cam.transform.eulerAngles = new Vector3(25.796f, 310.15f, 0);
        //    cam.orthographic = false;
            
        //}
        //else if (name.Equals("crossing"))
        //{
        //    Debug.Log("Not implemented yet!");
        //    cam.transform.position = new Vector3(12.92f, 15.16f, 31.19f);
        //    cam.transform.eulerAngles = new Vector3(25.796f, 310.15f, 0);
        //    cam.orthographic = false;

        //}
    }
}
