using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;
    public GameObject damageTextPrefab;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(int amount, Vector3 position)
    {
        GameObject obj = Instantiate(damageTextPrefab, position, Quaternion.identity);
        obj.GetComponent<DamageText>().SetText(amount.ToString());
    }
}
