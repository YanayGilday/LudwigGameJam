using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatsManager : MonoBehaviour
{
    public Text stats;
    public Text Name;
    public Image Avatar;

    private int dmg;
    private float speed;
    private float bulletPerSec;
    private int reloadTime;
    private int cartrageSize;
    private string name;
    private Sprite avatar;

    public GameObject oldGun;

    private void Start()
    {
        oldGun.GetComponent<WeaponStats>().Pick();
    }

    public void SetStats(GameObject weapon)
    {
        dmg = weapon.GetComponent<WeaponStats>().dmg;
        speed = weapon.GetComponent<WeaponStats>().speed;
        bulletPerSec = weapon.GetComponent<WeaponStats>().bulletPerSec;
        reloadTime = weapon.GetComponent<WeaponStats>().reloadTime;
        cartrageSize = weapon.GetComponent<WeaponStats>().cartrageSize;
        name = weapon.GetComponent<WeaponStats>().Wname;
        avatar = weapon.GetComponent<WeaponStats>().avatar;


        stats.text = dmg.ToString() + "\n" + speed.ToString() + "\n" + bulletPerSec.ToString();
        Name.text = name;
        Avatar.sprite = avatar;

    }
}
