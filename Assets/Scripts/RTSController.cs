using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RTSController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 cameraStartPosition;

    private List<PlayerUnit> selectedPlayerUnits;

    public RectTransform selectingBox;

    // Start is called before the first frame update
    void Start()
    {
        selectedPlayerUnits = new List<PlayerUnit>();
        selectingBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectingBox.gameObject.SetActive(true);
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cameraStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;

            float width = currentMousePosition.x - cameraStartPosition.x;
            float height = currentMousePosition.y - cameraStartPosition.y;

            selectingBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            selectingBox.anchoredPosition = cameraStartPosition + new Vector3(width / 2, height / 2);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectingBox.gameObject.SetActive(false);

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            foreach(PlayerUnit unit in selectedPlayerUnits)
            {
                unit.SetSelected(false);
            }
            selectedPlayerUnits.Clear();

            foreach(Collider2D collider2D in collider2DArray)
            {
                if(collider2D.tag == "PlayerUnit")
                {
                    selectedPlayerUnits.Add(collider2D.GetComponent<PlayerUnit>());
                    selectedPlayerUnits[selectedPlayerUnits.Count - 1].SetSelected(true);
                }
            }
            Debug.Log(selectedPlayerUnits.Count);
        }
    }
}
