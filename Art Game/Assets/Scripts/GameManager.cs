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
    public Canvas returnButton;
    public Canvas infoScreen;
    public TextMeshProUGUI infotext;
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
    public GameObject mainMenu;
    public GameObject homePage;
    public GameObject creditsPage;
    public GameObject settingsPage;
    public GameObject backButton;
    public GameObject paintings;
    public GameObject paintings2;
    public GameObject curPaint;
    public bool canClickOnPainting;
    public bool tileSlideStarted = false;

    private Vector3 camStartPos;
    private Quaternion camStartRot;
    private int ispyCounter;
    private int wordCounter;
    private int pbnCounter;
    private bool lerping = false;
    private bool lerpforward;

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
        if (lerping)
        {
            if (lerpforward)
            {

            }
            else
            {

            }
        }
    }


    public void play()
    {
        paintings.SetActive(true);
        paintings2.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void credits()
    {
        homePage.SetActive(false);
        creditsPage.SetActive(true);
        backButton.SetActive(true);
    }
    public void settings()
    {
        homePage.SetActive(false);
        settingsPage.SetActive(true);
        backButton.SetActive(true);
    }
    public void back()
    {
        homePage.SetActive(true);
        creditsPage.SetActive(false);
        settingsPage.SetActive(false);
        backButton.SetActive(false);
    }

    public void setLerp(bool val)
    {
        lerping = val;
    }
    public void setLerpForward(bool val)
    {
        lerpforward = val;
    }


    public void buttonPress(string name)
    {

        game = name;
        
        if (!lerping)
        {
            if (name.Equals("luncheon"))
            {

                cam.transform.position = new Vector3(-4f, 12f, -22f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (name.Equals("lilypads"))
            {
                cam.transform.position = new Vector3(-2.94f, 10.84f, -20.55f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.GetComponent<DragAndDrop>().enabled = true;
            }
            else if (name.Equals("and i was there"))
            {
                cam.transform.position = new Vector3(26.75f, 45.48f, -20.54f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (name.Equals("ice pies"))
            {
                cam.transform.position = new Vector3(21.3f, 73.1f, -22f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);

            }
            else if (name.Equals("crossing"))
            {
                cam.transform.position = new Vector3(59.5f, 10.2f, -9.6f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);



            }

            displayInfoText();
            cam.orthographic = true;
            if (name.Equals("crossing"))
            {
                cam.orthographic = false;
                if (!tileSlideStarted)
                {
                    slider.GetComponent<ST_PuzzleDisplay>().actualStart();
                    tileSlideStarted = true;
                }

            }
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
                        curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
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

    public void ispyfindWord(GameObject clicked)
    {
        foreach (Transform child in ispyWordList.transform)
        {
            if (child.name.Equals(clicked.name))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
                clicked.gameObject.GetComponent<BoxCollider>().enabled = false;
                ispyCounter += 1;
                if (ispyCounter == 8)
                {
                    curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
                    quitGame();
                }
            }
        }
    }

    public void wordsearchfindword(GameObject clicked)
    {
        foreach (Transform child in wordsearchWordList.transform)
        {
            if (child.name.Equals(clicked.name))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
                clicked.gameObject.GetComponent<BoxCollider>().enabled = false;
                wordCounter += 1;
                if (wordCounter == 8)
                {
                    curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
                    quitGame();
                }
            }
        }
    }


    public void tileComplete()
    {
        curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
        quitGame();
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
        curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
        return true;
    }

    public void displayInfoText()
    {
        returnButton.gameObject.active = false;
        infoScreen.gameObject.active = true;
        if (game.Equals("luncheon"))
        {

            infotext.text = "Tap on the hidden objects to complete the game!";
        }
        else if (game.Equals("lilypads"))
        {
            infotext.text = "Drag and Drop the jigsaw pieces into the correct position! Correctly placed pieces will stay in place.";
        }
        else if (game.Equals("and i was there"))
        {
            infotext.text = "Paint each section with the matching numbered paint. Tap on a paint to select it, then the painting to use it!";

        }
        else if (game.Equals("ice pies"))
        {
            infotext.text = "Tap the hidden words and phrases in the picture!";

        }
        else if (game.Equals("crossing"))
        {
            infotext.text = "Tap on the pieces to slide them over and try and get the original picture! The bottom left square will be missing when the puzzle is complete.";
        }
        Time.timeScale = 0f;
    }

    public void hideInfoText()
    {
        returnButton.gameObject.active = true;
        infoScreen.gameObject.active = false;
        Time.timeScale = 1f;
    }
    public void quitGame()
    {
        returnButton.gameObject.active = false;
        cam.GetComponent<DragAndDrop>().enabled = false;
        cam.transform.position = new Vector3(-49f, 4.92f, 51.43f);
        cam.transform.rotation = Quaternion.Euler(90f, 90f, 0);
        cam.orthographic = false;
        curPaint.GetComponent<Transition>().lerp = true;
        curPaint.GetComponent<Transition>().forward = false;
        curPaint.GetComponent<Transition>().stageOne = true;
        

    }
}
