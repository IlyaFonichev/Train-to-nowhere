using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryGenerator : MapGenerator
{
    [SerializeField]
    private GameObject bossRoom;
    [SerializeField]
    private GameObject chestRoom;
    [SerializeField]
    private GameObject mobsRoom;
    public override void CountRoomInitialization()
    {
        CountOfRooms = (uint)PlayerPrefs.GetInt("Depth") + 7 + (uint)Random.Range(0, 7);
    }
    public override void Generate()
    {
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
            AddLoadSceneIndicator();
            ProceduralGenerate(Random.Range(0, doors.Count));
        }
    }


    public override GameObject InstantiateRoom()
    {
        int numberRoom = Random.Range(0, 5);
        if (CurrentCountOfRooms == CountOfRooms - 2 && PlayerPrefs.GetInt("Depth") % 8 == 6)
            return Instantiate(bossRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
        else
        {
            switch (numberRoom)
            {
                case 0:
                    return Instantiate(chestRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
                default:
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
}
