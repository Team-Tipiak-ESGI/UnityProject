using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject healhBar;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDestroyed()) return;
        healhBar.GetComponent<Slider>().maxValue = player.GetComponent<Attack>().StartHealth;
        healhBar.GetComponent<Slider>().value = player.GetComponent<Attack>().health;
    }
}
