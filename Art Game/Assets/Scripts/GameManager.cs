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
    public Canvas pauseScreen;
    public TextMeshProUGUI infotext;
    public GameObject player;
    public Camera cam;
    public GameObject pbn;
    private Collider pcollider;
    private string game;
    public GameObject wall;
    private string curColor;
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
    public GameObject door;

    

    public AudioSource MainMenuMusic;
    public AudioSource GalleryMusic;
    public AudioSource CrossingMusic;
    public AudioSource LuncheonMusic;
    public AudioSource IcePiesMusic;
    public AudioSource LilypadsMusic;
    public AudioSource AndIWasThereMusic;
    private AudioSource currentMusic;
    public AudioSource doorSqueak;


    private Vector3 camStartPos;
    private Quaternion camStartRot;
    private int ispyCounter;
    private int wordCounter;
    private int pbnCounter;
    private int pbnCounterLimit;
    private bool lerping = false;
    private bool lerpforward;
    public int completedGames = 0;
    public bool doorOpen = false;
    public bool OpenTheDoor = false;
    public bool doorOpening = false;
    private float startTime;
    private bool gamePaused = false;
    private GameObject resumeButton;

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
        pbnCounterLimit = 0;
        StartCoroutine(FadeInMusic(MainMenuMusic));
        AudioListener.volume = 0.3f;
        resumeButton = pauseScreen.transform.GetChild(2).gameObject;
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (OpenTheDoor)
        {
            if (!doorOpening)
            {
                doorOpening = true;
                doorSqueak.Play();
                startTime = Time.time;
                
            }
            if (Time.time - startTime > 1f)
            {
                Quaternion lookOnLook = Quaternion.LookRotation(door.transform.position - cam.transform.position);

                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lookOnLook, Time.deltaTime * 1f);

                door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(0f, -90f, 0f), Time.deltaTime * 0.8f);

                if (door.transform.localRotation.y < -85)
                {
                    OpenTheDoor = false;
                }
            }
        }
        
    }

    public void pauseGame()
    {
        if (gamePaused)
        {
            resumeGame();
        }
        else
        {
            Time.timeScale = 0f;
            mainMenu.active = true;
            homePage.active = false;
            settingsPage.active = true;
            pauseScreen.gameObject.transform.GetChild(0).gameObject.active = false;
            pauseScreen.gameObject.transform.GetChild(1).gameObject.active = false;
            resumeButton.active = true;
            gamePaused = true;
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        mainMenu.active = false;
        homePage.active = false;
        settingsPage.active = false;
        pauseScreen.gameObject.transform.GetChild(0).gameObject.active = true;
        pauseScreen.gameObject.transform.GetChild(1).gameObject.active = true;
        resumeButton.active = false;
        gamePaused = false;
    }

    public IEnumerator FadeOutMusic(AudioSource source)
    {
        WaitForSeconds wait = new WaitForSeconds(0.04f);
        bool done = false;
        while (!done)
        {
            yield return wait;
            source.volume *= 0.95f;
            if (source.volume < 0.03f)
            {
                done = true;
                source.Stop();
            }
            
        }
    }
    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

    public IEnumerator FadeInMusic(AudioSource source)
    {
        currentMusic = source;
        WaitForSeconds wait = new WaitForSeconds(0.04f);
        bool done = false;
        source.volume = 0.03f;
        source.Play();
        while (!done)
        {
            yield return wait;
            source.volume *= 1.05f;
            if (source.volume > 0.95f)
            {
                done = true;
                source.volume = 1f;
            }

        }
    }

    public void play()
    {
        paintings.SetActive(true);
        paintings2.SetActive(true);
        mainMenu.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
        StartCoroutine(FadeOutMusic(MainMenuMusic));
        StartCoroutine(FadeInMusic(GalleryMusic));
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
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(LuncheonMusic));

            }
            else if (name.Equals("lilypads"))
            {
                cam.transform.position = new Vector3(-2.94f, 10.84f, -20.55f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.GetComponent<DragAndDrop>().enabled = true;
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(LilypadsMusic));
            }
            else if (name.Equals("and i was there"))
            {
                cam.transform.position = new Vector3(26.75f, 45.48f, -20.54f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(AndIWasThereMusic));
                pbnCounterLimit = 44;
            }
            else if (name.Equals("office pbn"))
            {
                
                cam.transform.position = new Vector3(47.640728f, 42.3699989f, -153.841095f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(AndIWasThereMusic));
                pbnCounterLimit = 75;
            }
            else if (name.Equals("museum pbn"))
            {
                cam.transform.position = new Vector3(135.779999f, 43.4300003f, -167.930954f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(AndIWasThereMusic));
                pbnCounterLimit = 78;
            }
            else if (name.Equals("ice pies"))
            {
                cam.transform.position = new Vector3(21.3f, 73.1f, -22f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(IcePiesMusic));

            }
            else if (name.Equals("crossing"))
            {
                cam.transform.position = new Vector3(59.5f, 10.2f, -9.6f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(CrossingMusic));



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

    // Redo this. Make it take transforms or colliders to get transform. no more of this searching shit.
    public void makeSpriteVisible(GameObject space)
    {
        
        if (space.GetComponent<ColorSetter>().getColor().Equals(curColor))
        {
            space.GetComponent<SpriteRenderer>().enabled = true;
            space.GetComponent<PolygonCollider2D>().enabled = false;
            space.GetComponentInChildren<TextMeshPro>().enabled = false;
            pbnCounter += 1;
            Debug.Log(pbnCounter);
            if (pbnCounter == pbnCounterLimit)
            {
                //TODO ADD THIS BACK IN
                //curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
                completedGames += 1;
                quitGame();
                        
                        
            }
        }
    }

    public void setColor(string name)
    {
        curColor = name;

    }

    public void ispyfindWord(GameObject clicked, GameObject WordList)
    {
        int ispyCounterMax = 0;
        foreach (Transform child in clicked.transform.parent)
        {
            ispyCounterMax += 1;
        }
        foreach (Transform child in WordList.transform)
        {
            if (child.name.Equals(clicked.name))
            {
                child.gameObject.GetComponent<TextMeshPro>().color = Color.gray;
                clicked.gameObject.GetComponent<BoxCollider>().enabled = false;
                ispyCounter += 1;
                if (ispyCounter == ispyCounterMax)
                {
                    curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
                    completedGames += 1;
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
                    completedGames += 1;
                    quitGame();
                    
                    

                }
            }
        }
    }


    public void tileComplete()
    {
        curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
        completedGames += 1;
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
        completedGames += 1;
        
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
        else
        {
            infotext.text = "No info here. Add some, developers!";
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
        cam.transform.rotation = Quaternion.Euler(90f, 90f, 0);
        cam.orthographic = false;
        cam.transform.position = new Vector3(-49f, 4.92f, 51.43f);
        curPaint.GetComponent<Transition>().lerp = true;
        curPaint.GetComponent<Transition>().forward = false;
        curPaint.GetComponent<Transition>().stageOne = true;
        StartCoroutine(FadeOutMusic(currentMusic));
        StartCoroutine(FadeInMusic(GalleryMusic));
        pbnCounter = 0;
        

    }

    
}
