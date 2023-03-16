using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject bulletPrefab;

    private float bulletSpeed = 5;
    private float bulletLifeSeconds = 1;
    private static int magazine = 30;
    private static int totalAmmo = 60;
    private float fireRate = 10;           // shots per second
    private float damage = 30;

    [SerializeField] GameObject textBox;
    [SerializeField] TextAsset textFile;

    private Vector3 canvasCenter;
    private PlayerController pc;
    private float delay = 0;
    private Text txt;

    private void Start()
    {
        string parameters = textFile.text;
        string[] arr = parameters.Split(';', 7);

        for (int i = 0; i < 7; i++)
            Debug.Log(arr[i]);
        //Debug.Log(string.Join("", parameters.Where(c => char.IsDigit(c))));

        pc = player.GetComponent<PlayerController>();
        canvasCenter = new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane);
        txt = textBox.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.mousePosition.x > canvasCenter.x)
        {
            transform.position = new Vector3(player.transform.position.x + 0.94f, player.transform.position.y, player.transform.position.z + -0.444f);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x - 0.94f, player.transform.position.y, player.transform.position.z + -0.444f);
            transform.rotation = Quaternion.Euler(-90, 0, -180);
        }

        if (delay > 1 / fireRate)
        {
            delay = 0;
            StartCoroutine(shoot());
        }
        else
            delay += Time.deltaTime;

        reload();
    }

    private IEnumerator shoot()
    {
        if (Input.GetAxisRaw("Fire") == 0) yield break;
        if (pc.getDash()) yield break;
        if (magazine == 0) yield break;

        Vector3 mousePos2D = Input.mousePosition;

        Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, playerCamera.nearClipPlane);

        Vector3 worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);
        Vector3 currCanvasCenter = playerCamera.ScreenToWorldPoint(canvasCenter);

        Vector3 mouseVector = worldPointPos - currCanvasCenter;
        mouseVector = new Vector3(mouseVector.x + Random.Range(-0.2f, 0.2f), 0, mouseVector.z + Random.Range(-0.2f, 0.2f));

        mouseVector.Normalize();


        GameObject bullet = Instantiate(bulletPrefab, transform.position + (mouseVector), new Quaternion(0, 0, 0, 0));
        bullet.tag = "Bullet";
        magazine--;
        printAmmo();

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        float elapsedtime = 0f;
        while (elapsedtime < bulletLifeSeconds)
        {
            if (bullet != null)
                bulletRb.velocity = mouseVector * bulletSpeed;
            else
                yield break;

            elapsedtime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(bullet);


        yield break;
    }

    public float getBulletLifeSeconds()
    {
        return bulletLifeSeconds;
    }

    private void reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && totalAmmo > 0 && magazine < 30)
        {
            if (totalAmmo + magazine >= 30)
            {
                totalAmmo -= 30 - magazine;
                magazine = 30;
            }
            else
            {
                magazine += totalAmmo;
                totalAmmo = 0;
            }
                        
            printAmmo();
        }
    }

    private void printAmmo()
    {
        txt.text = magazine.ToString() + " / " + totalAmmo.ToString();
    }
}
