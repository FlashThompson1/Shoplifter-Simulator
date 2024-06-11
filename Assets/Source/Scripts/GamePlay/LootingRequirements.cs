using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LootingRequirements : MonoBehaviour
{


    [SerializeField] private GameObject[] _products;
    [SerializeField] private GameObject[] _productsToStole;
    [SerializeField] private Image[] _objectiveImages;
    [Header("Objective Texts")]
    [SerializeField] private TextMeshProUGUI _firstProduct,_secondProduct,_thirdProduct;
    [SerializeField] private TextMeshProUGUI _objectiveTitleText;
    [SerializeField] private Image _compeleteImage;
    [Header("Arrow Sign")]
    [SerializeField] private GameObject _arrowSign;
    


    private Sprite _startObjectiveSprite;

    public int currentSequenceIndex = 0;


    private void Start()
    {
        RandomProductGenerator();
        _startObjectiveSprite = _objectiveImages[0].sprite;


    }

    private void Update()
    {
        ArrowSignActivator();
    }



    public void RandomProductGenerator() {

       

            _productsToStole[0] = _products[Random.Range(0, _products.Length)];
            _productsToStole[1] = _products[Random.Range(0, _products.Length)];
            _productsToStole[2] = _products[Random.Range(0, _products.Length)];

            _firstProduct.text = _productsToStole[0].name;
            _secondProduct.text = _productsToStole[1].name;
            _thirdProduct.text = _productsToStole[2].name;

        
    }


    public bool IsCollectable(RaycastHit hit) {
        for (int i = 0; i < _productsToStole.Length; i++)
        {
            if (hit.transform.name == _productsToStole[i].name)
                return true;
        }

        return false;
    }

    public void ObjectiveImages(GameObject[] _inventoryProducts) {
        int length = Mathf.Min(_productsToStole.Length, _inventoryProducts.Length, _objectiveImages.Length);

        for (int i = 0; i < length; i++)
        {
            if (_productsToStole[i].name == _inventoryProducts[i].name)
            {
                _objectiveImages[i].sprite = _compeleteImage.sprite;
                _objectiveImages[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        
    }



    public bool CheckingProductSebsequence(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;

        if (currentSequenceIndex <= 2) { 
            if (hitObject.name == _productsToStole[currentSequenceIndex].name)
            {
                return true;
            }
          }
            return false;
        
    }

    private void ArrowSignActivator() {

        if (currentSequenceIndex <= 2)
        {
            GameObject targetObject = GameObject.Find(_productsToStole[currentSequenceIndex].name);
            _arrowSign.transform.position = targetObject.transform.position + new Vector3(0, 3, 0);
        }
        else
            _arrowSign.SetActive(false);
    }

    /*
    public void MorseActivatorTexts() {
        _productsToStole[0] = _hiddenProduct;
        _firstProduct.text = _productsToStole[0].name;
        _objectiveImages[0].sprite = _startObjectiveSprite;
        _objectiveImages[1].gameObject.SetActive(false);
        _objectiveImages[2].gameObject.SetActive(false);
    }
    */


    public void ObjectiveTextChanger(GameObject[] products) {
        if(products.Length == 3)
        _objectiveTitleText.text = "Deliver Groceries To Car in Backyard";
        else
            _objectiveTitleText.text = "Products to Stole";
    }

    public void ObjectiveTextReturner()
    {
        for (int i = 0; i < _objectiveImages.Length; i++)
        {
            _objectiveImages[i].sprite = _startObjectiveSprite;
            _objectiveImages[i].transform.GetChild(0).gameObject.SetActive(true);

        }
    }
}
