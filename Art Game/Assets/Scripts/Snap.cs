using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    private Vector3 originalPos;
    private bool canSnap;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;
        randomizeLocation();
        canSnap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSnap && Vector3.Distance(gameObject.transform.position, originalPos) < 0.5f)
        {
            gameObject.transform.position = originalPos;
            canSnap = false;
            if (GameManager.Instance.checkPuzzle())
            {
                GameManager.Instance.quitGame();
            }
        }
    }


    public void randomizeLocation()
    {
        Vector3 tempPos = new Vector3(Random.Range(-15.5f, 11f), Random.Range(2f, 18f), 1.55f);
        gameObject.transform.position = tempPos;
    }

    public bool getcanSnap()
    {
        return canSnap;
    }
}
