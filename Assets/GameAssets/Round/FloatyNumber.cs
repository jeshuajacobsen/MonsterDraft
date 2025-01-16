using UnityEngine;
using TMPro;
public class FloatyNumber : MonoBehaviour
{
    private float FLOATY_NUMBER_LIFETIME = 1.5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 200);

        FLOATY_NUMBER_LIFETIME -= Time.deltaTime;
        if (FLOATY_NUMBER_LIFETIME <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void InitValues(int text, Vector2 startPosition, bool isDamage)
    {
        transform.position = startPosition;
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text + "";
        if(!isDamage)
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        else
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }
}
