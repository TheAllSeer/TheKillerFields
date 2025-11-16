using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;
    public TextMeshPro text;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
    public void SetText(string value)
    {
        text.text = value;
    }
}
