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
    public GameObject pbn;
    private Collider pcollider;
    private string game;
    private string curColor;
    public GameObject ispyWordList;
    public GameObject wordsearchWordList;

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
            cam.transform.position = new Vector3(-4f, 12f, -22f);
            cam.transform.eulerAngles = new Vector3(0, 180f, 0);
            game = name;
        }
        else if (name.Equals("lilypads"))
        {
            cam.transform.position = new Vector3(2.16f, 10.84f, -20.54f);
            game = name;
        }
        else if (name.Equals("and i was there"))
        {
            cam.transform.position = new Vector3(26.75f, 45.48f, -20.54f);
            game = name;
        }
        else if (name.Equals("ice pies"))
        {
            cam.transform.position = new Vector3(21.3f, 73.1f, -22f);
            cam.transform.eulerAngles = new Vector3(0, 180f, 0);
            game = name;

            }
            //else if (name.Equals("crossing"))
            //{
            //    Debug.Log("Not implemented yet!");
            //    cam.transform.position = new Vector3(12.92f, 15.16f, 31.19f);
            //    cam.transform.eulerAngles = new Vector3(25.796f, 310.15f, 0);
            //    cam.orthographic = false;

            //}
        }

    public string getGame()
    {
        
        return game;
    }

    public void makeSpriteVisible(string name)
    {
        foreach (Transform child in pbn.transform)
        {
            if (child.transform.name.Equals(name))
            {
                Debug.Log(3);
                Debug.Log(curColor);
                if (child.gameObject.GetComponent<ColorSetter>().getColor().Equals(curColor))
                {
                    Debug.Log(4);
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    public void setColor(string name)
    {
        Debug.Log(2);
        curColor = name;

    }

    public void ispyfindWord(string clicked)
    {
        foreach (Transform child in ispyWordList.transform)
        {
            if (child.name.Equals(clicked))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
            }
        }
    }

    public void wordsearchfindword(string clicked)
    {
        foreach (Transform child in wordsearchWordList.transform)
        {
            if (child.name.Equals(clicked))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
            }
        }
    }
}
