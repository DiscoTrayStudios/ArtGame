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
        originalPos = gameObject.transform.localPosition;
        randomizeLocation();
        canSnap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSnap && Vector3.Distance(gameObject.transform.localPosition, originalPos) < 0.5f)
        {
            gameObject.transform.localPosition = originalPos;
            canSnap = false;
            if (GameManager.Instance.checkPuzzle())
            {
                GameManager.Instance.quitGame();
            }
        }
    }


    public void randomizeLocation()
    {
        
        while (true)
        {
            float newx = Random.Range(-8.5f, 13f);
            float newy = Random.Range(-8f, 2.5f);
            if (newx < -4.5f || newx > 9.5f || newy < -7.5f || newy > 1f)
            {
                
                    Vector3 tempPos = new Vector3(newx, newy, 0f);
                    gameObject.transform.localPosition = tempPos;
                    break;
                
            }
        }
        
    }

    public bool getcanSnap()
    {
        return canSnap;
    }
}
