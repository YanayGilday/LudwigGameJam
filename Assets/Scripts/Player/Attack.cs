using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float RmouseX;
    private float RmouseY;

    bool aboveF;
    bool aboveG;

    public Transform playerTransform;
    public Transform gunTransform;
    public Transform shieldTransform;

    private float Rdirection;
    private string direction;

    public int dmg;
    public float speed;
    public float bulletPerSec;
    private int reloadTime;
    private int cartrageSize;
    public GameObject shootPoint;

    private bool ableToShoot = true;
    private bool inShieldMod = false;

    public GameObject prefabBullet;
    private GameObject bullet;
    public Transform gunDirection;

    private GameObject currentWeapon;
    private GameObject lastWeapon;


    public GameObject gameManager;

    private Sprite upSprite;
    private Sprite rightSprite;
    private Sprite leftSprite;
    private Sprite downSprite;
    private Sprite currentSprite;

    public float XpixelOffset;
    public float YpixelOffset;

    private int Direction;

    public Transform rotationSetter;


    void Update()
    {
        CheckDirection();
        this.GetComponent<SpriteRenderer>().sprite = currentSprite;

        if (Input.GetKey(KeyCode.Mouse0) && ableToShoot == true && inShieldMod == false)
        {
            ableToShoot = false;
            StartCoroutine(Shoot());
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            inShieldMod = true;
            gunTransform.position = new Vector3(1000000, 1000000, 1000000);
            shieldTransform.gameObject.GetComponent<Collider2D>().enabled = true;
            shieldTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            float x = (XpixelOffset / 32) * Direction;
            float y = (YpixelOffset / 32);
            inShieldMod = false;
            gunTransform.position = new Vector3(playerTransform.position.x + x, playerTransform.position.y + y, 0) ;
            shieldTransform.gameObject.GetComponent<Collider2D>().enabled = false;
            shieldTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Direction = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Direction = -1;
        }

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
        rotationSetter.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle +180));
    }
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    public void CheckDirection()
    {
        RmouseX = Input.mousePosition.x;
        RmouseY = Input.mousePosition.y;

        if (RmouseX * 0.5625 > RmouseY)
        {
            aboveF = false;
        }
        else
        {
            aboveF = true;
        }
        if (RmouseX * -0.5625 + 1080 > RmouseY)
        {
            aboveG = false;
        }
        else
        {
            aboveG = true;
        }

        if (aboveF == true && aboveG == true)
        {
            currentSprite = upSprite;
            direction = "up";
            gunDirection.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (aboveF == false && aboveG == true)
        {
            currentSprite = rightSprite;
            direction = "right";
            gunDirection.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (aboveF == true && aboveG == false)
        {
            currentSprite = leftSprite;
            direction = "left";
            gunDirection.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (aboveF == false && aboveG == false)
        {
            currentSprite = downSprite;
            direction = "down";
            gunDirection.eulerAngles = new Vector3(0, 0, -90);
        }
    }

    public void PickWeapon(GameObject weapon)
    {
        lastWeapon = currentWeapon;
        currentWeapon = weapon;

        currentWeapon.transform.position = new Vector3(1000000, 1000000, 1000000);
        if (lastWeapon != null)
        {
            lastWeapon.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            lastWeapon.transform.position = new Vector3(playerTransform.position.x + 1, playerTransform.position.y, 0);
            lastWeapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        dmg = weapon.GetComponent<WeaponStats>().dmg;
        speed = weapon.GetComponent<WeaponStats>().speed;
        bulletPerSec = weapon.GetComponent<WeaponStats>().bulletPerSec;
        reloadTime = weapon.GetComponent<WeaponStats>().reloadTime;
        cartrageSize = weapon.GetComponent<WeaponStats>().cartrageSize;

        upSprite = weapon.GetComponent<WeaponStats>().upSprite;
        rightSprite = weapon.GetComponent<WeaponStats>().rightSprite;
        leftSprite = weapon.GetComponent<WeaponStats>().leftSprite;
        downSprite = weapon.GetComponent<WeaponStats>().downSprite;

        gameManager.GetComponent<WeaponStatsManager>().SetStats(weapon);
    }

    IEnumerator Shoot()
    {
        bullet = Instantiate(prefabBullet, shootPoint.transform.position, rotationSetter.rotation) as GameObject;
        bullet.transform.position = shootPoint.transform.position;
        bullet.GetComponent<Bullet>().BulletShot(dmg, speed, direction, this.tag);

        yield return new WaitForSeconds(1/bulletPerSec);
        
        ableToShoot = true;
    }
}
