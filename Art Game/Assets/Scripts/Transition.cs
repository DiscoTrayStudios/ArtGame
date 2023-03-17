using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Transition : MonoBehaviour
{
    // ADD ABILITY TO CHOOSE BETWEEN CAMERA AND PAINTINGS, AND ADD START AND END COORD FOR BOTH
    public bool lerp;
    public bool forward;
    public bool stageOne = true;

    public Sprite badPic;
    public Sprite goodPic;

    private Vector3 paintPos;
    private Quaternion paintRot;
    private Vector3 lerpPos1;
    private Vector3 lerpPos2;
    private Vector3 camLerpPos;
    private Vector3 camoriPos;
    private Quaternion camoriRot;
    private Vector3 paintoriPos;
    private Quaternion paintoriRot;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        paintPos = transform.localPosition;
        paintoriPos = transform.localPosition;
        paintoriRot = transform.localRotation; ;
        paintRot = transform.localRotation;
        GameObject c = GameObject.Find("Camera");
        cam = c.GetComponent<Camera>();
        camoriPos = GameManager.Instance.camStartPos;
        camoriRot = GameManager.Instance.camStartRot;
        stageOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerp)
        {
            if (forward)
            {
                Vector3 targetCameraPosition = transform.position + transform.forward * 2f;

                // Lerp the camera's position towards the target position
                cam.transform.position = Vector3.Lerp(cam.transform.position, targetCameraPosition, Time.deltaTime * 1.2f);
                cam.transform.LookAt(transform.position);

                // If the camera has reached the target position, stop moving it
                if (Vector3.Distance(cam.transform.position,targetCameraPosition) < 0.1)
                {
                    forward = false;
                    lerp = false;
                    GameManager.Instance.curPaint = gameObject;
                    GameManager.Instance.buttonPress(transform.parent.gameObject.name);
                }
                
            }
            else
            {

                GameManager.Instance.canClickOnPainting = false;
                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camoriPos, Time.deltaTime * 1.2f);
                cam.transform.localRotation = Quaternion.Lerp(cam.transform.rotation, camoriRot, Time.deltaTime * 1.2f);
                if (Vector3.Distance(cam.transform.localPosition, camoriPos) < 0.01)
                {
                    forward = true;
                    lerp = false;
                    cam.transform.localRotation = camoriRot;

                    GameManager.Instance.canClickOnPainting = true;
                }
            }
        }
    }
    //world position = UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-52.65999984741211,"y":6.130000114440918,"z":54.12000274658203},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":0.048193126916885379,"y":0.05072213336825371,"z":0.05072213336825371}}
}
