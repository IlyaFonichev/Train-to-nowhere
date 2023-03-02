using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    /// 0 - пустая
    /// 1 - стартовая комната
    /// 2 - лайт с мобами
    /// 3 - сокровищнеца
    /// 4 - с боссом
    /// </summary>
    private int lines = 0, columns = 0;
    private int countOfRooms, currentCountOfRooms;
    private int[,] roomPosition;
    [SerializeField]
    private int maxCountBoss, maxCountChest;
    private void Start()
    {
        lines = countOfRooms = Random.Range(5, 10);
        columns = countOfRooms = Random.Range(5, 10);
        roomPosition = new int[lines, columns];
        SetRooms();

        DrawMap();
    }
    private void SetRooms()
    {
        int flagChestRoom = 0, flagBossRoom = 0;

        //Задали начальную комнату
        int startRoomPosX = 0;
        int startRoomPosY = Random.Range(0, lines);
        roomPosition[startRoomPosY, startRoomPosX] = 1;

        //Сетаем все остальные комнаты
        Vector2 currentroomPosition = new Vector2(startRoomPosX, startRoomPosY); //Текущая комната

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (countOfRooms == currentCountOfRooms)
                    break;
                int direction = Random.Range(0, 3); //0 - вниз, 1 - направо, 2 - вверх
                int roomID = 0;
                int temp = 0;
                while (temp == 0)
                {
                    temp = Random.Range(2, 5);
                    if (temp == 4 && flagBossRoom < maxCountBoss)
                        flagBossRoom++;
                    if (temp == 3 && flagChestRoom < maxCountChest)
                        flagChestRoom++;
                }
                roomID = temp;

                //Переполнение чек
                switch (direction)
                {
                    case 0:
                        if (currentroomPosition.y + 1 > lines)
                            break;
                        else
                        {
                            if (roomPosition[(int)currentroomPosition.x, (int)currentroomPosition.y + 1] != 0)
                                break;
                            else
                                currentroomPosition.y++;
                            break;
                        }
                    case 1:
                        if (currentroomPosition.x + 1 > columns)
                            break;
                        else
                        {
                            if (roomPosition[(int)currentroomPosition.x + 1, (int)currentroomPosition.y] != 0)
                                break;
                            else
                                currentroomPosition.x++;
                            break;
                        }
                    case 2:
                        if (currentroomPosition.y - 1 < 0)
                            break;
                        else
                        {
                            if (roomPosition[(int)currentroomPosition.x, (int)currentroomPosition.y - 1] != 0)
                                break;
                            else
                                currentroomPosition.y--;
                            break;
                        }
                }

                roomPosition[(int)currentroomPosition.x, (int)currentroomPosition.y] = roomID;
                countOfRooms++;
            }
            if (countOfRooms == currentCountOfRooms)
                break;
        }
    }

    private void DrawMap()
    {

    }
}
