using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public void SetMaxHealth(int maxHealth)
    {
        SetHealth(maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        fillImage.fillAmount = Mathf.Clamp01((float)currentHealth / 10f);
    }
}
