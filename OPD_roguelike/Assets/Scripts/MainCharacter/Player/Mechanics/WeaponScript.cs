using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    private GameObject player;
    private Camera playerCamera;

    public GameObject bulletPrefab;

    private float bulletSpeed = 5;
    private float bulletLifeSeconds = 1;
    private int magazine = 30;
    private int totalAmmo = 60;
    private float fireRate = 10;           // shots per second
    private float damage = 30;
    private float accuracy = 0.2f;
    private bool isMelee = false;
    private float range = 0;

    [HideInInspector] public int currAmmo = -1;
    [HideInInspector] public int currMagazine = -1;

    GameObject textBox;

    private Vector3 mouseVector;

    private PlayerController pc;
    private float delay = 0;
    private Text txt;

    private Quaternion weaponAngle;
    private float cameraDistance;

    private bool isReloading = false;

    private Player stats;

    private void Start()
    {
        player = PlayerController.instance.gameObject;
        playerCamera = CameraController.instance.gameObject.GetComponent<Camera>();
        textBox = GameObject.Find("Ammo");

        applyParameters();

        pc = player.GetComponent<PlayerController>();
        txt = textBox.GetComponent<Text>();
        stats = player.GetComponent<Player>();

        cameraDistance = playerCamera.gameObject.GetComponent<CameraController>().DeltaPosition.magnitude;

        printAmmo();
    }

    private void OnLevelWasLoaded(int level)
    {
        textBox = GameObject.Find("Ammo");
        txt = textBox.GetComponent<Text>();

        printAmmo();
    }

    private void Update()
    {
        calculateMouseVector();

        pointWeaponToMouse();

        if (delay > 1 / (fireRate * stats.rateOfFireMultiplayer) && !isReloading && (PauseManager.instance == null || !PauseManager.instance.GetComponent<PauseManager>().onPause))
        {
            if (!isMelee)
                StartCoroutine(shoot());    // ranged attack
            else
                StartCoroutine(attack());   // melee attack
        }
        else
            delay += Time.deltaTime;

        StartCoroutine(reload());
    }

    private IEnumerator shoot()  // ranged attack
    {
        if (Input.GetAxisRaw("Fire") == 0) yield break;
        if (pc.getDash()) yield break;
        if (currMagazine == 0) yield break;

        delay = 0;

        Vector3 mouseShootVector = mouseVector.normalized;
        Vector3 shootVector = new Vector3(mouseShootVector.x + Random.Range(-accuracy, accuracy), 0, mouseShootVector.z + Random.Range(-accuracy, accuracy));
        shootVector.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, transform.position + (shootVector), new Quaternion(0, 0, 0, 0));
        currMagazine--;
        printAmmo();

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        float elapsedtime = 0f;
        while (elapsedtime < bulletLifeSeconds)
        {
            if (bullet != null)
                bulletRb.velocity = shootVector * bulletSpeed;
            else
                yield break;

            elapsedtime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(bullet);

        yield break;
    }

    private IEnumerator attack()    // melee attack
    {
        if (Input.GetAxisRaw("Fire") == 0) yield break;
        if (pc.getDash()) yield break;

        delay = 0;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, new Quaternion(0, 0, 0, 0));

        bullet.transform.localScale = new Vector3(range, range, 0.1f);
        bullet.transform.rotation = weaponAngle;

        yield return new WaitForSeconds(0.1f);

        Destroy(bullet);

        yield break;
    }

    private IEnumerator reload()
    {
        if (!Input.GetKeyDown(KeyCode.R)) yield break;
        if (currAmmo == 0) yield break;
        if (currMagazine == magazine) yield break;

        isReloading = true;
        txt.text = "Reloading...";

        yield return new WaitForSeconds(1f);

        if (currAmmo > 0 && currMagazine < magazine)
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
        }

        printAmmo();
        isReloading = false;

        yield break;
    }
    
    public void printAmmo()
    {
        if (!isMelee)
            txt.text = currMagazine.ToString() + " / " + currAmmo.ToString();
        else
            txt.text = "";
    }

    private void applyParameters()
    {
        float[] parsedParams = new float[7];
        GetComponent<Item>().MakeParamsArray().CopyTo(parsedParams, 0);

        magazine = (int)parsedParams[0];
        totalAmmo = (int)parsedParams[1];
        fireRate = parsedParams[2];
        bulletSpeed = parsedParams[3];
        range = parsedParams[4];
        damage = parsedParams[5];
        accuracy = parsedParams[6];

        if (magazine == 0)
            isMelee = true;

        if (!isMelee)
            bulletLifeSeconds = range / bulletSpeed;

        if (currAmmo == -1)
        {
            currAmmo = totalAmmo;
            currMagazine = magazine;
        }
    }

    private void calculateMouseVector()
    {
        Vector3 mousePos2D = Input.mousePosition;

        Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, cameraDistance);

        Vector3 worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);

        mouseVector = worldPointPos - gameObject.transform.position;

        mouseVector = new Vector3(mouseVector.x, 0, mouseVector.z);

        //mouseVector.Normalize();
    }

    private void pointWeaponToMouse()
    {
        Vector3 playerMouseVector = (mouseVector + gameObject.transform.position - player.transform.position);
        playerMouseVector = new Vector3(playerMouseVector.x, 0, playerMouseVector.z).normalized;

        Vector3 angleMouseVector = mouseVector.normalized;

        if (playerMouseVector.x > 0)
        {
            transform.position = new Vector3(player.transform.position.x + 0.872f, player.transform.position.y, player.transform.position.z + -0.188f);

            if (angleMouseVector.z > 0 && angleMouseVector.z < 0.2f)
                transform.rotation = Quaternion.Euler(90, -(int)Vector3.Angle(Vector3.right, angleMouseVector), 0);
            else if (angleMouseVector.z > 0)
                transform.rotation = Quaternion.Euler(-90, -(int)Vector3.Angle(Vector3.right, angleMouseVector), 0);
            else
                transform.rotation = Quaternion.Euler(90, (int)Vector3.Angle(Vector3.right, angleMouseVector), 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x - 0.872f, player.transform.position.y, player.transform.position.z + -0.188f);

            if (angleMouseVector.z > 0 && angleMouseVector.z < 0.2f)
                transform.rotation = Quaternion.Euler(-90, (int)Vector3.Angle(Vector3.left, angleMouseVector), -180);
            else if (angleMouseVector.z > 0)
                transform.rotation = Quaternion.Euler(90, (int)Vector3.Angle(Vector3.left, angleMouseVector), -180);
            else
                transform.rotation = Quaternion.Euler(-90, -(int)Vector3.Angle(Vector3.left, angleMouseVector), -180);
        }

        weaponAngle = transform.rotation;
    }

    public float getBulletLifeSeconds()
    {
        return bulletLifeSeconds;
    }

    public float getDamage()
    {
        return damage * stats.damageDealMultiplayer;
    }

    public bool getReloading()
    {
        return isReloading;
    }

    public int getTotalAmmo()
    {
        return totalAmmo;
    }

    public int getMagazine()
    {
        return magazine;
    }
}
