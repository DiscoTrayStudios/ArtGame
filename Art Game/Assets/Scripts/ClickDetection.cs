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
            Debug.Log(hit.collider.name);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("gallery"))
                {
                    Debug.Log("YAY");
                    move = true;
                    temp = hit.collider.gameObject;
                    
                }
                else if (hit.collider.CompareTag("ispy"))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.CompareTag("painting"))
                {
                    GameManager.Instance.buttonPress(hit.collider.name);
                }
                else if (hit.collider.CompareTag("pbn"))
                {
                    Debug.Log(hit.collider.name);
                }
            }
            

        }

        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, endpos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, end.transform.rotation, Time.deltaTime * 1.5f);
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
