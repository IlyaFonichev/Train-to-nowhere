using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastelandGenerator : MapGenerator
{
    private int numberOfCurrentParentDoor;
    [SerializeField]
    private GameObject bossRoom;
    [SerializeField]
    private GameObject chestRoom;
    [SerializeField]
    private GameObject mobsRoom;
    public override void CountRoomInitialization()
    {
        CountOfRooms = (uint)PlayerPrefs.GetInt("WastelandDepth") + 7 + (uint)Random.Range(0, 5);;
    }

    public override void Generate()
    {
        numberOfCurrentParentDoor = 0;
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
            ProceduralGenerate(numberOfCurrentParentDoor);
            numberOfCurrentParentDoor++;
        }
    }

    public override GameObject InstantiateRoom()
    {
        if (CurrentCountOfRooms == CountOfRooms - 2)
        {
            if(PlayerPrefs.GetInt("WastelandDepth") % 5 == 4)
                return Instantiate(bossRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
            else
                return Instantiate(chestRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
        }
        else
        {
            int number = Random.Range(0, 5);
            switch (number)
            {
                case 0:
                    return Instantiate(EmptyRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
                default:
                    return Instantiate(mobsRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
            }
        }
    }
}
