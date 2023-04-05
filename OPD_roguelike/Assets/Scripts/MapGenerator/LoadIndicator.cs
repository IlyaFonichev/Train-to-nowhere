using UnityEngine;
using UnityEngine.UI;

public class LoadIndicator : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (MapGenerator.instance != null)
        {
            GetComponent<Image>().fillAmount = MapGenerator.instance.Load / 100;
        }
    }
}
