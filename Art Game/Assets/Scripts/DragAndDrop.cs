using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject selectedPiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedPiece == null)
            {
                RaycastHit  hit= castray();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("puzzle"))
                    {
                        return;
                    }
                    selectedPiece = hit.collider.gameObject; 
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedPiece.transform.position).z);
                Vector3 worldpos = Camera.main.ScreenToWorldPoint(position);
                selectedPiece.transform.position = new Vector3(worldpos.x, worldpos.y, worldpos.z);

                selectedPiece = null;
                Cursor.visible = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedPiece.transform.position).z);
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(position);
            if (selectedPiece != null)
            {
                selectedPiece.transform.position = new Vector3(worldpos.x, worldpos.y, 1.55f);
                selectedPiece.GetComponent<SpriteMask>().alphaCutoff = 0.2f;
            }
            selectedPiece = null;
            Cursor.visible = true;
        }


        if (selectedPiece != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedPiece.transform.position).z);
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(position);
            selectedPiece.transform.position = new Vector3(worldpos.x, worldpos.y, -0.25f);
            selectedPiece.GetComponent<SpriteMask>().alphaCutoff = 1f;


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
