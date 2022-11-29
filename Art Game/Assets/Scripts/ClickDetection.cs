using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    private bool move;
    public GameObject end;
    public float enddist;
    public float back;
    private Vector3 endpos;
    private GameObject temp;
    public GameObject wordlist;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        endpos = end.transform.position;
        endpos.z = endpos.z - back;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = castray();
            
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("gallery"))
                {
                    Debug.Log("YAY");
                    move = true;
                    temp = hit.collider.gameObject;
                    GameManager.Instance.canClickOnPainting = true;

                }
                else if (hit.collider.CompareTag("ispy"))
                {
                    GameManager.Instance.ispyfindWord(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("painting"))
                {
                    if (GameManager.Instance.canClickOnPainting)
                    {
                        hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<Transition>().lerp = true;
                        hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<Transition>().forward = true;
                        //GameManager.Instance.buttonPress(hit.collider.name);
                    }
                }
                else if (hit.collider.CompareTag("wordsearch"))
                {
                    GameManager.Instance.wordsearchfindword(hit.collider.gameObject);
                }
                

            }
            else if (GameManager.Instance.getGame() == "and i was there")
            {
                Debug.Log(-2);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
                
                if (hit2d.collider != null)
                {
                    Debug.Log(-1);
                    if (hit2d.collider.CompareTag("pbn"))
                    {
                        GameManager.Instance.makeSpriteVisible(hit2d.collider.name);
                    }
                    else if (hit2d.collider.CompareTag("color"))
                    {
                        Debug.Log(1);
                        GameManager.Instance.setColor(hit2d.collider.name);
                        //hit2d.collider.gameObject  Outline stuff goes here
                    }
                }

            }


        }

        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, endpos, Time.deltaTime * 1.75f);
            transform.rotation = Quaternion.Lerp(transform.rotation, end.transform.rotation, Time.deltaTime * 2f);
            if (Vector3.Distance(transform.position, endpos) < enddist)
            {
                move = false;
                temp.GetComponent<BoxCollider>().enabled = false;

            }
        }
    }

    private RaycastHit castray()
    {
        Vector3 screenmouseposfar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenmouseposnear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldmouseposfar = Camera.main.ScreenToWorldPoint(screenmouseposfar);
        Vector3 worldmouseposnear = Camera.main.ScreenToWorldPoint(screenmouseposnear);
        RaycastHit hit;
        Physics.Raycast(worldmouseposnear, worldmouseposfar - worldmouseposnear, out hit);
        return hit;
    }


}
