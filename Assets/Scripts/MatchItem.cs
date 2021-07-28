using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchItem hoverItem;

    public GameObject linePrefab;
    public string itemLabel;
    
    private GameObject line;

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        if(GetComponentInChildren<Line>())
        {
            Destroy(GetComponentInChildren<Line>().gameObject);
            Debug.Log("Found and destroyed line");
        }
        line = Instantiate(linePrefab, transform.position, Quaternion.identity);
        line.transform.SetParent(transform);
        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        if(!this.Equals(hoverItem))
        {
            MatchManager.Instance.lines.Add(line);
            UpdateLine(hoverItem.transform.position);

            if(itemLabel.Equals(hoverItem.itemLabel))
            {
                GameController.Instance.AddPoint();
            }else
            {
                GameController.Instance.SubstractPoint();
            }
            //Destroy(hoverItem);
            //Destroy(this);
        }else
        {
            Destroy(line);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
        hoverItem = this;
    }

    private void UpdateLine(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
    }
}
