using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardImageController : MonoBehaviour
{
    [SerializeField] private Card cardObject;
    [SerializeField] private Image _image;

    public void setCardObject(Card cardObject)
    {
        this.cardObject = cardObject;
    }

    private void OnEnable()
    {
        _image.sprite = cardObject.image;
        GetComponent<MatchItem>().itemLabel = cardObject.label;
    }
}
