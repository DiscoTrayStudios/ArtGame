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
    public GameObject wall;
    private string curColor;
    public GameObject ispyWordList;
    public GameObject wordsearchWordList;
    public GameObject puzzlePieces;
    public GameObject slider;

    private Vector3 camStartPos;
    private Quaternion camStartRot;
    private int ispyCounter;
    private int wordCounter;
    private int pbnCounter;

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
        camStartPos = cam.transform.position;
        camStartRot = cam.transform.rotation;
        ispyCounter = 0;
        wordCounter = 0;
        pbnCounter = 0;
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
            cam.GetComponent<DragAndDrop>().enabled = true;
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
        else if (name.Equals("crossing"))
        {
            cam.transform.position = new Vector3(59.5f, 10.2f, -9.6f);
            cam.transform.eulerAngles = new Vector3(0.0488541909f, 359.9f, 0.0226515327f);
            cam.orthographic = false;
            slider.GetComponent<ST_PuzzleDisplay>().actualStart();

        }
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
                if (child.gameObject.GetComponent<ColorSetter>().getColor().Equals(curColor))
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    child.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    child.gameObject.GetComponentInChildren<TextMeshPro>().enabled = false;
                    pbnCounter += 1;
                    Debug.Log(pbnCounter);
                    if (pbnCounter == 44)
                    {
                        quitGame();
                    }
                }
            }
        }
    }

    public void setColor(string name)
    {
        curColor = name;

    }

    public void ispyfindWord(string clicked)
    {
        foreach (Transform child in ispyWordList.transform)
        {
            if (child.name.Equals(clicked))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
                ispyCounter += 1;
                if (ispyCounter == 8)
                {
                    quitGame();
                }
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
                wordCounter += 1;
                if (wordCounter == 8)
                {
                    quitGame();
                }
            }
        }
    }



    public bool checkPuzzle()
    {
        foreach (Transform child in puzzlePieces.transform)
        {
            foreach(Transform grandchild in child)
            {
                if (grandchild.gameObject.GetComponent<Snap>().getcanSnap())
                {
                    return false; ;
                }
            }
        }
        return true;
    }


    public void quitGame()
    {
        cam.GetComponent<DragAndDrop>().enabled = false;
        Debug.Log("YOUUUUUUUU");
        cam.transform.position = camStartPos;
        cam.transform.rotation = camStartRot;
        cam.orthographic = false;
        wall.GetComponent<BoxCollider>().enabled = true;
    }
}
