using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangeCreater : MonoBehaviour
{
    [Header("Generators"), SerializeField]
    private GameObject training;
    [SerializeField]
    private GameObject cave, forest, wasteland, laboratory;

    public void CreateDange(string dange)
    {
        switch (dange)
        {
            case "Training":
                Instantiate(training, Vector3.zero, Quaternion.identity);
                break;
            case "Cave":
                Instantiate(cave, Vector3.zero, Quaternion.identity);
                break;
            case "Forest":
                Instantiate(forest, Vector3.zero, Quaternion.identity);
                break;
            case "Wasteland":
                Instantiate(wasteland, Vector3.zero, Quaternion.identity);
                break;
            case "Laboratory":
                Instantiate(laboratory, Vector3.zero, Quaternion.identity);
                break;
        }
    }
}
