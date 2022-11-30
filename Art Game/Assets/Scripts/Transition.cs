using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    public bool lerp;
    public bool forward;
    public bool stageOne = true;

    public Sprite badPic;
    public Sprite goodPic;

    private Vector3 oriPos;
    private Quaternion oriRot;
    private Vector3 lerpPos1;
    private Vector3 lerpPos2;
    private Vector3 camLerpPos;
    private Vector3 camoriPos;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.localPosition;
        oriRot = transform.rotation;
        Debug.Log(oriRot);
        lerpPos1 = new Vector3(0.635f, 0.183f, -1.5f);
        lerpPos2 = new Vector3(0.675f, -0.075f, -1.3651f);
        camLerpPos = new Vector3(-49f, 4.92f, 51.43f);
        GameObject c = GameObject.Find("Camera");
        cam = c.GetComponent<Camera>();
        camoriPos = new Vector3(-55.5026817f, 3.55660272f, 51.8777657f);
        stageOne = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerp)
        {
            if (forward)
            {
                if (stageOne)
                {
                    GameManager.Instance.canClickOnPainting = false;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, lerpPos1, Time.deltaTime*1.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 90f, transform.eulerAngles.z), Time.deltaTime * 1.5f);
                    cam.transform.LookAt(transform);
                    if (Vector3.Distance(transform.localPosition, lerpPos1) < 0.01)
                    {
                        stageOne = false;
                    }
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, lerpPos2, Time.deltaTime * 1.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 90f, transform.eulerAngles.z), Time.deltaTime * 1.3f);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, camLerpPos, Time.deltaTime*1.3f);
                    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(90f, 90f, transform.eulerAngles.z), Time.deltaTime * 1.3f);
                    //cam.transform.LookAt(transform);
                    if (Vector3.Distance(transform.localPosition, lerpPos2) < 0.01)
                    {
                        forward = false;
                        lerp = false;
                        GameManager.Instance.curPaint = gameObject;
                        GameManager.Instance.buttonPress(transform.parent.gameObject.name);
                    }
                }
            }
            else
            {
                if (stageOne)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, lerpPos1, Time.deltaTime * 1.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90f, transform.eulerAngles.z), Time.deltaTime * 1.5f);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, camoriPos, Time.deltaTime * 1.3f);
                    //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(0f, 90f, transform.eulerAngles.z), Time.deltaTime * 1.3f);
                    cam.transform.LookAt(transform);
                    if (Vector3.Distance(transform.localPosition, lerpPos1) < 0.01)
                    {
                        stageOne = false;
                    }
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, oriPos, Time.deltaTime * 1.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 1.5f);
                    cam.transform.LookAt(transform);
                    if (Vector3.Distance(transform.localPosition, oriPos) < 0.01)
                    {
                        stageOne = true;
                        lerp = false;
                        forward = true;
                        cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        if (GameManager.Instance.completedGames == 4)
                        {
                            if (!GameManager.Instance.doorOpen)
                            {
                                GameManager.Instance.OpenTheDoor = true;
                            }
                            else
                            {
                                GameManager.Instance.canClickOnPainting = true;
                            }
                        }
                        else
                        {
                            GameManager.Instance.canClickOnPainting = true;
                        }
                        
                        //GameManager.Instance.wall.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
    }
    //world position = UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-52.65999984741211,"y":6.130000114440918,"z":54.12000274658203},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":0.048193126916885379,"y":0.05072213336825371,"z":0.05072213336825371}}
}
