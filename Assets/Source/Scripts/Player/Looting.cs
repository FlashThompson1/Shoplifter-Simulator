using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Looting : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _collectableObject;
    [SerializeField] private LayerMask _doorLayer;
    [SerializeField] private Camera _camera;
 



    [Header("Button Active Section")]
    [SerializeField] private Animator _buttonAnimator;


    [Header("Stolen Products")]
    [SerializeField] private GameObject[] _stolenProducts;

   // [Header("Product Stolen Counter Text")]
   // [SerializeField] private TextMeshProUGUI _productStolenCounterText;

    private LootingRequirements _lootingRequirements;
    private int _productCount = 0;
    [SerializeField]private int _requireProductStoleCount;
    //private int _maxstolecount = 10;


    [SerializeField]private AudioSource _audiosource;

    public Slider progressBar;
    public float pickupDuration = 2f; 
    public bool isPressing = false; 
    public bool isResponsed = false;
    private float pressTime = 0f; 


    private void Start()
    {
        
        _lootingRequirements = GameObject.FindObjectOfType<LootingRequirements>();
       // _productStolenCounterText.text = _requireProductStoleCount.ToString();
       // _productStolenCounterText.text = $"{_requireProductStoleCount}/{_maxstolecount}";
        
    }

    private void Update()
    {
        _lootingRequirements.ObjectiveTextChanger(_stolenProducts);
        //WinGame();
        CheckingObject();
        //MorseRunner();
       

    }




    private void CheckingObject() {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range, _collectableObject) && _lootingRequirements.IsCollectable(hit) && _lootingRequirements.CheckingProductSebsequence(hit))
        {
            if (_stolenProducts.Length < 3)
            {
                _buttonAnimator.SetBool("IsActive", true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isPressing = true;
                    pressTime = 0f;


                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    isPressing = false;
                    progressBar.value = 0f;
                    if (progressBar.value == 0)
                        progressBar.gameObject.SetActive(false);
                }
                if (isPressing)
                {
                    pressTime += Time.deltaTime;
                    progressBar.value = pressTime / pickupDuration;
                    progressBar.gameObject.SetActive(true);

                    if (pressTime >= pickupDuration)
                    {
                        isPressing = false;
                        isResponsed = true;
                        progressBar.gameObject.SetActive(false);
                        progressBar.value = 0f;
                        AddProduct(hit.collider.gameObject);
                        hit.collider.gameObject.SetActive(false);
                        _lootingRequirements.currentSequenceIndex++;
                        _lootingRequirements.ObjectiveImages(_stolenProducts);
                        _audiosource.Play();
                    }
                }
            }
        }
        else
        {
            _buttonAnimator.SetBool("IsActive", false);
            progressBar.value = 0f;
            isPressing = false;
            progressBar.gameObject.SetActive(false);
        }

    }





  private void AddProduct(GameObject product)
    {

        System.Array.Resize(ref _stolenProducts, _productCount + 1);
        _stolenProducts[_productCount] = product;
        _productCount++;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out LootingRequirements managertruck) && _stolenProducts.Length == 3) {


            /*
            DeleteAllGameObjects();
            managertruck.RandomProductGenerator();
            managertruck.ObjectiveTextReturner();
            managertruck.currentSequenceIndex = 0;
            _requireProductStoleCount++;
            _productStolenCounterText.text = $"{_requireProductStoleCount}/{_maxstolecount}";
            */

            WinGame();
        }
    }



    private void WinGame() {
       // if (_requireProductStoleCount == 10)
            LoadScene.Instance.LoadNextScene(2);



    }

    /*

    public void DeleteAllGameObjects()
    {
        
        foreach (GameObject obj in _stolenProducts)
        {
            if (obj != null)
            {
                Destroy(obj);
               
            }
        }

        StartCoroutine(ClearArrayNextFrame());
    }

  

    private IEnumerator ClearArrayNextFrame()
    {
        yield return new WaitForEndOfFrame();
        _productCount = 0;
        _stolenProducts = new GameObject[0];  
        
    }

    
    private void MorseRunner() {


        if (_requireProductStoleCount == 9) {
            DeleteAllGameObjects();
            _lootingRequirements.currentSequenceIndex = 0;
            _lootingRequirements.MorseActivatorTexts();

        }
    
    }

    */

private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * _range);
    }
}
