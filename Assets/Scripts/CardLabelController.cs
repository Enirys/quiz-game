using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardLabelController : MonoBehaviour
{
    [SerializeField] private Card cardObject;
    [SerializeField] private TextMeshProUGUI _labeltxt;

    public void setCardObject(Card cardObject)
    {
        this.cardObject = cardObject;
    }

    private void OnEnable()
    {
        _labeltxt.text = cardObject.label;
        GetComponent<MatchItem>().itemLabel = cardObject.label;
    }
}
