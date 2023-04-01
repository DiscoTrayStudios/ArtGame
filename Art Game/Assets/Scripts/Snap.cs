using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snap : MonoBehaviour
{
    private Vector3 originalPos;
    private bool canSnap;
    public float maxdistance;
    public GameObject target;
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
        int count = 0;
        while (true)
        {
            count++;
            if (count == 1000)
            {
                Debug.Log("FUCKLSDFJLKSDJFLKSDJF");
                break;
            }
            float newx = Random.Range(-maxdistance, maxdistance);
            float newy = Random.Range(-maxdistance, maxdistance);
            //if (newx < -4.5f || newx > 9.5f || newy < -7.5f || newy > 1f)
            //{
                
            //        Vector3 tempPos = new Vector3(newx, newy, 0f);
            //        gameObject.transform.localPosition = tempPos;
            //        break;
                
            //}
            RaycastHit hit;
            Vector3 tempPos = new Vector3(newx, newy, 0f);
            transform.localPosition = tempPos;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name.Equals(target.name))
                {
                    Debug.Log("HERE HER HER EHRKLSJLKJSDLKFKLSDFJ");
                    break;
                }
                
            }
        }

        
        
    }

    public bool getcanSnap()
    {
        return canSnap;
    }
}
