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
    private int magazine = 30;
    private int totalAmmo = 60;
    private float fireRate = 10;           // shots per second
    private float damage = 30;
    private float accuracy = 0.2f;
    private bool isMelee = false;
    private float range = 0;

    private int currAmmo;
    private int currMagazine;

    [SerializeField] GameObject textBox;

    private Vector3 canvasCenter;
    private PlayerController pc;
    private float delay = 0;
    private Text txt;

    private void Start()
    {
        applyParameters();
        
        pc = player.GetComponent<PlayerController>();
        canvasCenter = new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane);
        txt = textBox.GetComponent<Text>();

        printAmmo();
    }

    private void Update()
    {
        if (Input.mousePosition.x > canvasCenter.x)
        {
            transform.position = new Vector3(player.transform.position.x + 0.956f, player.transform.position.y, player.transform.position.z + -0.049f);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x - 0.956f, player.transform.position.y, player.transform.position.z + -0.049f);
            transform.rotation = Quaternion.Euler(-90, 0, -180);
        }

        if (delay > 1 / fireRate)
        {
            if (!isMelee)
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
        if (currMagazine == 0) yield break;

        delay = 0;

        Vector3 mousePos2D = Input.mousePosition;

        Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, playerCamera.nearClipPlane);

        Vector3 worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);
        Vector3 currCanvasCenter = playerCamera.ScreenToWorldPoint(canvasCenter);

        Vector3 mouseVector = worldPointPos - currCanvasCenter;
        mouseVector = new Vector3(mouseVector.x + Random.Range(-accuracy, accuracy), 0, mouseVector.z + Random.Range(-accuracy, accuracy));

        mouseVector.Normalize();


        GameObject bullet = Instantiate(bulletPrefab, transform.position + (mouseVector), new Quaternion(0, 0, 0, 0));
        currMagazine--;
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

    private void reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currAmmo > 0 && currMagazine < magazine)
        {
            if (currAmmo + currMagazine >= magazine)
            {
                currAmmo -= magazine - currMagazine;
                currMagazine = magazine;
            }
            else
            {
                currMagazine += currAmmo;
                currAmmo = 0;
            }

            printAmmo();
        }
    }

    public void printAmmo()
    {
        txt.text = currMagazine.ToString() + " / " + currAmmo.ToString();
    }

    public void applyParameters()
    {
        float[] parsedParams = new float[7];
        GetComponent<Item>().MakeParamsArray().CopyTo(parsedParams, 0);

        magazine = (int)parsedParams[0];
        totalAmmo = (int)parsedParams[1];
        fireRate = parsedParams[2];
        bulletSpeed = parsedParams[3];
        bulletLifeSeconds = parsedParams[4];
        damage = parsedParams[5];
        accuracy = parsedParams[6];

        currAmmo = totalAmmo;
        currMagazine = magazine;
    }

    public float getBulletLifeSeconds()
    {
        return bulletLifeSeconds;
    }

    public float getDamage()
    {
        return damage;
    }
}
