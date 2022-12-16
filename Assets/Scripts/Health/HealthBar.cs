using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHeal;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHeal.currentHealth / 10 ;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = playerHeal.currentHealth / 10;
    }
}
