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

    private void OnDestroy()
    {
        PauseManager.instance.IsInstantiate = true;
        PlayerController.instance.IsInstantiate = true;
    }
}
