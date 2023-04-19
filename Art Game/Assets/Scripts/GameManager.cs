using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    public Canvas returnButton;
    public Canvas infoScreen;
    public Canvas pauseScreen;
    public TextMeshProUGUI infotext;
    public Camera cam;
    private Collider pcollider;
    private string game;
    public GameObject paintings;
    private string curColor;
    public GameObject wordsearchWordList;
    public GameObject puzzlePieces;
    public GameObject slider;
    public GameObject slider2;
    public GameObject mainMenu;
    public GameObject homePage;
    public GameObject creditsPage;
    public GameObject settingsPage;
    public GameObject backButton;
    public GameObject curPaint;
    public bool canClickOnPainting;
    public bool tileSlideStarted = false;
    public UnityEngine.UI.Button nextLevelButton;


    

    public AudioSource MainMenuMusic;
    public AudioSource GalleryMusic;
    public AudioSource TileMusic;
    public AudioSource IspyMusic;
    public AudioSource JigsawMusic;
    public AudioSource PBNMusic;
    public int whoDidIt;
    public GameObject clueObjects;
    public GameObject toggableLights;
    private AudioSource currentMusic; 


    public Vector3 camStartPos;
    public Quaternion camStartRot;
    private int ispyCounter;
    private int wordCounter;
    private int pbnCounter;
    private int pbnCounterLimit;
    private bool lerping = false;
    private bool lerpforward;
    public int completedGames = 0;
    private float startTime;
    private bool gamePaused = false;
    private GameObject resumeButton;
    private bool completed_game_just_now = false;
    
    private Vector3 lastpos;
    private Quaternion lastrot;



    private Scene curScene;
    private ProgressBar progressBar;
    private string newScene;

    public GameObject dialogueCanvasObject;
    private Start_Dialogue start_Dialogue;
    private Friend_Dialogue friend_Dialogue;
    private Office_Dialogue office_Dialogue;
    private Museum_Dialogue museum_Dialogue;
    private End_Dialogue end_Dialogue;
    private List<Tuple<string, string, string>> dialogueList;
    private string fullDialogueText;
    private string whoIsSpeaking;
    private bool isTyping = false;
    private bool isItalic = false;
    private bool isBold = false;
    private bool isLeaving = false;
    private bool isTooLong = false;
    private bool isWaitingBetweenChars = false;
    private float timeBetweenChars = 0.03f;
    private bool isIntro = true;
    private int currentDialogueIndex;
    private TextMeshProUGUI dialogueText;
    private TextMeshProUGUI nameText;

    public GameObject mainTextObject;
    public GameObject nameTextObject;

    public GameObject peopleObject;
    public Sprite kristenNormal;
    public Sprite kristenShocked;
    public Sprite kristenHappy;
    public Sprite kristenAngry;
    public Sprite craneNormal;
    public Sprite craneShocked;
    public Sprite craneHappy;
    public Sprite craneAngry;
    public Sprite burchNormal;
    public Sprite burchShocked;
    public Sprite burchHappy;
    public Sprite burchAngry;
    private Dictionary<string, Dictionary<string, Sprite>> whichPicture;

    private void Awake()
    {
        if (!Instance)
        {
            Debug.Log("it went with this");
            Instance = this;
            DontDestroyOnLoad(this);
            whoDidIt = UnityEngine.Random.RandomRange(0, 3);
            Debug.Log(whoDidIt);
        }
        else
        {
            Debug.Log("THis p[art works");
            //GameManager temp = Instance;
            //Instance = this;
            //DontDestroyOnLoad (this);
            //whoDidIt = temp.whoDidIt;
            //Destroy(temp);
            //Debug.Log(whoDidIt);
            this.whoDidIt = Instance.whoDidIt;
            Destroy(Instance); 
            Instance = this;
            DontDestroyOnLoad(this);
            Debug.Log(whoDidIt);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        curScene = SceneManager.GetActiveScene();
        camStartPos = cam.transform.position;
        camStartRot = cam.transform.rotation;
        ispyCounter = 0;
        wordCounter = 0;
        pbnCounter = 0;
        pbnCounterLimit = 0;
        StartCoroutine(FadeInMusic(MainMenuMusic));
        AudioListener.volume = 0.3f;
        resumeButton = pauseScreen.transform.GetChild(2).gameObject;
        loadClueObjects();
        start_Dialogue  = new Start_Dialogue();
        friend_Dialogue = new Friend_Dialogue();
        office_Dialogue  = new Office_Dialogue();
        museum_Dialogue = new Museum_Dialogue();
        end_Dialogue    = new End_Dialogue();
        dialogueText = mainTextObject.GetComponent<TextMeshProUGUI>();
        nameText = nameTextObject.GetComponent<TextMeshProUGUI>();
        //kristenNormal = Resources.Load<Sprite>("Images/Characters/kristenWalker");
        whichPicture = new Dictionary<string, Dictionary<string, Sprite>>();
        Dictionary<string, Sprite> kdict = new Dictionary<string, Sprite>();
        Dictionary<string, Sprite> cdict = new Dictionary<string, Sprite>();
        Dictionary<string, Sprite> bdict = new Dictionary<string, Sprite>();
        kdict.Add("normal", kristenNormal);
        kdict.Add("angry", kristenAngry);
        kdict.Add("shocked", kristenShocked);
        kdict.Add("happy", kristenHappy);
        cdict.Add("normal", craneNormal);
        cdict.Add("angry", craneAngry);
        cdict.Add("shocked", craneShocked);
        cdict.Add("happy", craneHappy);
        bdict.Add("normal", burchNormal);
        bdict.Add("angry", burchAngry);
        bdict.Add("shocked", burchShocked);
        bdict.Add("happy", burchHappy);
        whichPicture.Add("Kristen", kdict);
        whichPicture.Add("Crane", cdict);
        whichPicture.Add("Burch", bdict);
        dialogueList = start_Dialogue.get_dialogue(completedGames, whoDidIt);




    }
    

    // Load the new scene asynchronously
    public void AsyncLoadScene()
    {

        if (curScene.name.Equals("Apartment"))
        {
            newScene = "Office";

        }
        else if (curScene.name.Equals("Office"))
        {
            newScene = "Museum";
        }
        StartCoroutine(LoadSceneAsync());
    }

    // Coroutine to load the new scene asynchronously
    private IEnumerator LoadSceneAsync()
    {
        // Create an async operation to load the scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(newScene);

        // While the operation is not yet complete, update the progress bar
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //progressBar.SetValueWithoutNotify(progress);
            yield return null;
        }
        curScene = SceneManager.GetSceneByName(newScene);
        
    }

    public void loadClueObjects()
    {
        // whoDidIt = 0 = Kristen
        // whoDidIt = 1 = Crane
        // whoDidIt = 2 = Burch
            // Burch child 0 = Did do it
            // Burch child 1 = Did not do it


        toggableLights.SetActive(false);
        clueObjects.transform.GetChild(2).gameObject.SetActive(true);
        if (whoDidIt.Equals(2))
        {
            clueObjects.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            clueObjects.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            clueObjects.transform.GetChild(0).gameObject.SetActive(false);
            clueObjects.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            clueObjects.transform.GetChild(whoDidIt).gameObject.SetActive(true);
            clueObjects.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);

            clueObjects.transform.GetChild(Math.Abs(whoDidIt-1)).gameObject.SetActive(false);
            clueObjects.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }
        if (whoDidIt.Equals(1))
        {
            toggableLights.SetActive(true);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (dialogueCanvasObject.active){
            if (Input.GetMouseButtonDown(0))
            {
                if (isTyping && !isTooLong)
                {
                    //if (fullDialogueText.Length > 200)
                    //{
                    //    timeBetweenChars = 0f;
                    //}
                    //else
                    //{
                    //    StopCoroutine("typeText");
                    //    dialogueText.text = fullDialogueText;
                    //    isTyping = false;
                    //} 
                    isWaitingBetweenChars = false;
                    
                }
                else if (!isTooLong)
                {
                    timeBetweenChars = 0.03f;
                    if (currentDialogueIndex < dialogueList.Count-1) {

                        currentDialogueIndex += 1;
                        nameText.text = dialogueList[currentDialogueIndex].Item1;
                        fullDialogueText = dialogueList[currentDialogueIndex].Item3;
                        set_sprite(dialogueList[currentDialogueIndex].Item1, dialogueList[currentDialogueIndex].Item2);

                        StartCoroutine("typeText");
                    }
                    else
                    {
                        quitDialogue();
                    }
                }
            }
        }
        
    }



    public void set_sprite(string whoIsSpeaking, string emotion)
    {
        Debug.Log("Sprite0");
        if (whichPicture.Keys.Contains(whoIsSpeaking))
        {
            Debug.Log("Sprite1");
            Sprite sprite = whichPicture[whoIsSpeaking][emotion];
            bool replaced = false;
            bool alreadyShown = false;
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Sprite2");
                UnityEngine.UI.Image image = peopleObject.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Image>();
                image.gameObject.SetActive(true);
                
                if (image.sprite != null )
                {
                    string sprite_name = image.sprite.name.ToLower();
                    if (sprite_name.Contains(whoIsSpeaking.ToLower()))
                    {
                        Debug.Log("Sprite6");
                        if (!replaced)
                        {
                            Debug.Log("Sprite7");
                            image.sprite = sprite;
                            replaced = true;
                        }
                        else
                        {
                            Debug.Log("SpriteS8");
                            image.sprite = null;
                        }
                    }
                }
                else if (image.sprite == null)
                {
                    Debug.Log("Sprite3");
                    if (!replaced)
                    {
                        Debug.Log("Sprite4");
                        image.sprite = sprite;
                        replaced = true;
                    }
                    else
                    {
                        Debug.Log("Sprite5");
                        image.gameObject.SetActive(false);
                    }

                }
               
                
                
            }
        }
        else { Debug.Log("Sprite0"); }
    }
    public void completed_game_dialogue()
    {
        dialogueCanvasObject.SetActive(true);
        currentDialogueIndex = 0;
        dialogueList.Clear();
        canClickOnPainting = false;

        currentDialogueIndex = 0;
        if (curScene.name.Equals("Apartment"))
        {

            dialogueList = friend_Dialogue.get_dialogue(completedGames, whoDidIt);
            nameText.text = dialogueList[currentDialogueIndex].Item1;
            //TODO: Put sprite stuff for character images here.
            set_sprite(dialogueList[currentDialogueIndex].Item1, dialogueList[currentDialogueIndex].Item2);
            fullDialogueText = dialogueList[currentDialogueIndex].Item3;
            StartCoroutine("typeText");
        }
        else if (curScene.name.Equals("Office"))
        {
            
            dialogueList = office_Dialogue.get_dialogue(completedGames, whoDidIt);
            nameText.text = dialogueList[currentDialogueIndex].Item1;
            set_sprite(dialogueList[currentDialogueIndex].Item1, dialogueList[currentDialogueIndex].Item2);
            fullDialogueText = dialogueList[currentDialogueIndex].Item3;
            StartCoroutine("typeText");
        }
        else if (curScene.name.Equals("Museum"))
        {

            dialogueList = museum_Dialogue.get_dialogue(completedGames, whoDidIt);
            nameText.text = dialogueList[currentDialogueIndex].Item1;
            set_sprite(dialogueList[currentDialogueIndex].Item1, dialogueList[currentDialogueIndex].Item2);
            fullDialogueText = dialogueList[currentDialogueIndex].Item3;
            StartCoroutine("typeText");
        }
        else
        {
            dialogueCanvasObject.SetActive(false);
        }
        
    }
    public void quitDialogue()
    {
        if (isIntro) 
        {
            isIntro = false;
            completed_game_dialogue();
        }
        else
        {
            canClickOnPainting = true;
            dialogueCanvasObject.SetActive(false);
            foreach (Transform child in peopleObject.transform)
            {
                child.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = null;
                child.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator typeText()
    {
        
        List<string> textList = fullDialogueText.Split(' ').ToList<string>();
        isTyping = true;
        dialogueText.text = "";
        int curChars = 0;
        foreach (string word in textList)
        {
            if (curChars + word.Length > 200)
            {
                while (!Input.GetMouseButtonDown(0))
                {
                    isTooLong = true;
                    yield return null;
                }
                timeBetweenChars = 0.03f;
                isTooLong = false;
                curChars = 0;
                dialogueText.text = "";

                isWaitingBetweenChars = true;
            }
            dialogueText.text += " ";
            foreach (char c in word)
            {
                dialogueText.text += c;
                curChars++;
                if (isWaitingBetweenChars)
                {
                    yield return new WaitForSeconds(timeBetweenChars);
                }
            }
        }

        isWaitingBetweenChars = true;
        isTyping = false;
        yield return null;


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
        mainMenu.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
        StartCoroutine(FadeOutMusic(MainMenuMusic));
        StartCoroutine(FadeInMusic(GalleryMusic));
        dialogueCanvasObject.SetActive(true);
        dialogueList = start_Dialogue.get_dialogue(completedGames, whoDidIt);
        currentDialogueIndex = 0;
        fullDialogueText = dialogueList[currentDialogueIndex].Item3;
        canClickOnPainting = false;
        StartCoroutine("typeText");
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
            lastpos = cam.transform.position;
            lastrot = cam.transform.rotation;
            if (name.Equals("apartment ispy"))
            {

                cam.transform.position = new Vector3(-88.4400024f, 11.9799995f, -18.2500267f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(IspyMusic));
                cam.orthographicSize = 13.5f;

            }
            else if (name.Equals("office ispy"))
            {

                cam.transform.position = new Vector3(-4f, 12f, -22f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(IspyMusic));
                cam.orthographicSize = 13.5f;

            }
            else if (name.Equals("museum ispy"))
            {
                cam.transform.position = new Vector3(-152.56752f, 12.3245363f, -41.417923f);
                cam.transform.rotation = Quaternion.Euler(0, 180f, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(IspyMusic));
                cam.orthographicSize = 13.5f;

            }
            else if (name.Equals("apartment jigsaw"))
            {
                cam.transform.position = new Vector3(-3.43000007f, -59.2799988f, -28.0927353f);
                cam.transform.rotation = Quaternion.Euler(0, 0f, 0);
                cam.orthographicSize = 15f;
                cam.GetComponent<DragAndDrop>().enabled = true;
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(JigsawMusic));
            }
            else if (name.Equals("office jigsaw"))
            {
                cam.transform.position = new Vector3(-5.71000004f, -95.5100021f, -52.3827896f);
                cam.transform.rotation = Quaternion.Euler(0, 0f, 0);
                cam.orthographicSize = 15f;
                cam.GetComponent<DragAndDrop>().enabled = true;
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(JigsawMusic));
            }
            else if (name.Equals("museum jigsaw"))
            {
                cam.transform.position = new Vector3(-2.6400001f, -25.0900002f, -36.5200005f);
                cam.transform.rotation = Quaternion.Euler(0, 0f, 0);
                cam.orthographicSize = 15f;
                cam.GetComponent<DragAndDrop>().enabled = true;
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(JigsawMusic));
            }
            else if (name.Equals("lilypads"))
            {
                cam.transform.position = new Vector3(-2.94f, 10.84f, -20.55f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.GetComponent<DragAndDrop>().enabled = true;
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(JigsawMusic));
            }
            else if (name.Equals("apartment pbn"))
            {

                cam.transform.position = new Vector3(29.2000008f, 45.2999992f, -26.8999996f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(PBNMusic));
                pbnCounterLimit = 44;
                cam.orthographicSize = 13.5f;
            }
            else if (name.Equals("office pbn"))
            {

                cam.transform.position = new Vector3(47.640728f, 42.3699989f, -153.841095f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(PBNMusic));
                pbnCounterLimit = 75;
                cam.orthographicSize = 13.5f;
            }
            else if (name.Equals("museum pbn"))
            {
                cam.transform.position = new Vector3(135.779999f, 43.4300003f, -167.930954f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(PBNMusic));
                pbnCounterLimit = 78;
            }

            else if (name.Equals("crossing"))
            {
                cam.transform.position = new Vector3(59.5f, 10.2f, -9.6f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(TileMusic));
            }
            else if (name.Equals("apartment tile"))
            {
                cam.transform.position = new Vector3(64.1976471f, 110.110001f, 125.779999f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(TileMusic));
            }
            else if (name.Equals("office tile"))
            {
                cam.transform.position = new Vector3(64.2286453f, 85f, 126.970001f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(TileMusic));
            }
            else if (name.Equals("museum tile1"))
            {
                
                cam.transform.position = new Vector3(64.3500595f, 59.959999f, 127.110001f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(TileMusic));
            }
            else if (name.Equals("museum tile2"))
            {

                cam.transform.position = new Vector3(64.3600006f, 135.020004f, 126.650002f);
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeOutMusic(currentMusic));
                StartCoroutine(FadeInMusic(TileMusic));
            }

            displayInfoText();
            cam.orthographic = true;
            if (name.Equals("crossing") || name.Equals("apartment tile") || name.Equals("office tile") || name.Equals("museum tile1") || name.Equals("museum tile2"))
            {
                cam.orthographic = false;
                if (!tileSlideStarted)
                {
                    if (name.Equals("museum tile2"))
                    {
                        slider2.GetComponent<ST_PuzzleDisplay>().actualStart();
                        tileSlideStarted = true;
                    }
                    else
                    {
                        slider.GetComponent<ST_PuzzleDisplay>().actualStart();
                        tileSlideStarted = true;
                    }
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
                curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
                completedGames += 1;
                completed_game_just_now = true;
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
                    completed_game_just_now = true;
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
                    completed_game_just_now = true;
                    quitGame();
                    
                    

                }
            }
        }
    }


    public void tileComplete()
    {
        curPaint.GetComponent<SpriteRenderer>().sprite = curPaint.GetComponent<Transition>().goodPic;
        curPaint.GetComponent<SpriteRenderer>().transform.localScale = Vector3.one;
        completedGames += 1;
        tileSlideStarted = false;
        completed_game_just_now = true;
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
        completed_game_just_now = true;
        curPaint.GetComponent<SpriteRenderer>().transform.localScale = Vector3.one;
        // pieces.transform.parent.parent.gameObject.GetComponent<BoxCollider>().active = false;
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
        cam.transform.position = lastpos;
        cam.transform.rotation = lastrot;
        returnButton.gameObject.active = false;
        cam.GetComponent<DragAndDrop>().enabled = false;
        cam.orthographic = false;
        curPaint.GetComponent<Transition>().lerp = true;
        curPaint.GetComponent<Transition>().forward = false;
        curPaint.GetComponent<Transition>().stageOne = true;
        StartCoroutine(FadeOutMusic(currentMusic));
        StartCoroutine(FadeInMusic(GalleryMusic));
        pbnCounter = 0;
        UnityEngine.Cursor.visible = true;
        if (completed_game_just_now)
        {
            completed_game_just_now = false;
            completed_game_dialogue();
        }
        if ((completedGames > 0&& curScene.name != "Museum"))
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else if (nextLevelButton)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    

    public void resetCam()
    {
        cam.transform.rotation = camStartRot;
        cam.orthographic = false;
        cam.transform.position = camStartPos;
    }


    public List<List<float>> getJigsawRange(string n)
    {
        List<List<float>> total = new List<List<float>>();
        List<float> outer = new List<float>();
        List<float> inner = new List<float>();
        if (n.Equals("museum jigsaw"))
        {
            outer.Add(-7f);
            outer.Add(7f);
            outer.Add(-20f);
            outer.Add(-30f);
            inner.Add(-5f);
            inner.Add(5f);
            inner.Add(-22f);
            inner.Add(-28);
            total.Add(outer);
            total.Add(inner);
            return total;
        }
        return total;
    }

    
}
