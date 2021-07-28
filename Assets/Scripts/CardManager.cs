using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    [SerializeField] private List<Card> _allCards = new List<Card>();
    [SerializeField] private List<Card> _usedCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();
    [SerializeField] private List<Transform> _selectedImageCardsTransforms = new List<Transform>();
    [SerializeField] private List<Transform> _selectedLabelsCardsTransforms = new List<Transform>();
    [SerializeField] private List<string> _selectedLabels = new List<string>();

    [SerializeField] private GameObject _imagesPanel;
    [SerializeField] private GameObject _labelsPanel;
    [SerializeField] private GameObject _cardImagePrefab;
    [SerializeField] private GameObject _cardLabelPrefab;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;    
        InitializeCards();
    }

    public void InitializeCards()
    {
        if(_usedCards.Count <= 0)
        {
            foreach (Card card in _allCards)
            {
                _usedCards.Add(card);
            }
        }
    }

    public void GenerateCards()
    {
        if(GameController.Instance._gameReplay)
        {
            AddCards(_selectedCards);
        }

        if(_selectedImageCardsTransforms.Count > 0)
        {
            DeleteCards();
        }

        if(_selectedLabelsCardsTransforms.Count > 0)
        {
            DeleteCards();
        }

        SelectRandomCards();
        ShowSelectedCards(_selectedCards);
    }

    private void AddCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            _usedCards.Add(card);
        }
        _usedCards = ShuffleList(_usedCards);
    }

    private void SelectRandomCards()
    {
        if(_usedCards.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, _usedCards.Count);
                _selectedCards.Add(_usedCards[randomIndex]);
                _selectedLabels.Add(_usedCards[randomIndex].label);
                _usedCards.Remove(_usedCards[randomIndex]);
            }
        }else
        {
            Debug.Log("List is now empty!");
        }
    }

    private void ShowSelectedCards(List<Card> selectedCards)
    {
        foreach (var card in selectedCards)
        {
            CreateImageCard(card);
        }
        selectedCards = ShuffleList(selectedCards);
        foreach (var card in selectedCards)
        {
            CreateLabelCard(card);
        }
    }

    private void CreateImageCard(Card selectedCard)
    {
        GameObject card = Instantiate(_cardImagePrefab, transform.position, Quaternion.identity);
        _selectedImageCardsTransforms.Add(card.transform);
        card.transform.SetParent(_imagesPanel.transform);
        card.GetComponent<CardImageController>().setCardObject(selectedCard);
        card.SetActive(true);
    }

    private void CreateLabelCard(Card selectedCard)
    {
        GameObject card = Instantiate(_cardLabelPrefab, transform.position, Quaternion.identity);
        _selectedLabelsCardsTransforms.Add(card.transform);
        card.transform.SetParent(_labelsPanel.transform);
        card.GetComponent<CardLabelController>().setCardObject(selectedCard);
        card.SetActive(true);
    }

    private void DeleteCards()
    {
        foreach (var cardImageTransform in _selectedImageCardsTransforms)
        {
            Destroy(cardImageTransform.gameObject);
        }

        foreach (var cardLabelTransform in _selectedLabelsCardsTransforms)
        {
            Destroy(cardLabelTransform.gameObject);
        }

        _selectedCards.Clear();
        _selectedImageCardsTransforms.Clear();
        _selectedLabels.Clear();
        _selectedLabelsCardsTransforms.Clear();
    }

    private List<Card> ShuffleList(List<Card> list)
    {
        List<Card> shuffledList = new List<Card>();
        var rnd = new System.Random();
        shuffledList = list.OrderBy(item => rnd.Next()).ToList();
        return shuffledList;
    }
}
