using UnityEngine;

public class PortalOpener : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab;
    private bool portalIsOpen;

    private void Update()
    {
        //Это так для демонстарции
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!portalIsOpen)
            {
                GameObject portal = Instantiate(portalPrefab, RoomSwitcher.getCurrentRoom.transform.position, Quaternion.identity);
                portal.transform.SetParent(RoomSwitcher.getCurrentRoom.transform);
                portalIsOpen = true;
            }
            else
                Debug.Log("Портал открыт!");
        }
    }
}
