using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    private bool move;
    public float enddist;
    public float back;
    private Vector3 endpos;
    private Quaternion endrot;
    private GameObject temp;
    public GameObject wordlist;
    private GameObject prevOutline;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        endpos = GameManager.Instance.cam.transform.position;
        endrot = GameManager.Instance.cam.transform.rotation;
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
                    GameManager.Instance.ispyfindWord(hit.collider.gameObject, hit.collider.transform.parent.parent.Find("Words").gameObject);
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
            else if ((GameManager.Instance.getGame() == "apartment pbn") || (GameManager.Instance.getGame() == "office pbn") || (GameManager.Instance.getGame() == "museum pbn"))
            {
                Debug.Log(-2);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
                
                if (hit2d.collider != null)
                {
                    Debug.Log(-1);
                    if (hit2d.collider.CompareTag("pbn"))
                    {
                        GameManager.Instance.makeSpriteVisible(hit2d.collider.gameObject);
                    }
                    else if (hit2d.collider.CompareTag("color"))
                    {
                        GameObject outline = hit2d.collider.gameObject.transform.GetChild(1).gameObject;
                        if (prevOutline != null)
                        {
                            prevOutline.active = false;
                        }
                        prevOutline = outline;
                        prevOutline.active = true;
                        GameManager.Instance.setColor(hit2d.collider.name);
                    }
                }

            }


        }

        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-55.5030632f, 3.55635023f, 51.5f), Time.deltaTime * 1.75f);
            transform.rotation = Quaternion.Lerp(transform.rotation, endrot, Time.deltaTime * 2f);
            if (Vector3.Distance(transform.position, new Vector3(-55.5030632f, 3.55635023f, 51.5f)) < enddist)
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
        Debug.DrawRay(worldmouseposnear, worldmouseposfar - worldmouseposnear, Color.red);
        return hit;
    }


}
