using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetector : MonoBehaviour
{
	public int distance = 3;

    public void Update()
    {
		FindClosestweapon();
		FindClosestfood();

	}
    void FindClosestweapon()
	{
		float distanceToClosestfunc = Mathf.Infinity;
		WeaponStats closestfunc = null;
		WeaponStats[] allfuncs = GameObject.FindObjectsOfType<WeaponStats>();

		foreach (WeaponStats currentfunc in allfuncs)
		{
			float distanceTofunc = (currentfunc.transform.position - this.transform.position).sqrMagnitude;
			if (distanceTofunc < distanceToClosestfunc)
			{
				distanceToClosestfunc = distanceTofunc;
				closestfunc = currentfunc;
			}
		}

		if (distanceToClosestfunc < distance)
		{
			Debug.DrawLine(this.transform.position, closestfunc.transform.position);
			if (Input.GetKeyDown(KeyCode.E))
            {
				closestfunc.GetComponent<WeaponStats>().Pick();
            }
		}
	}

	void FindClosestfood()
	{
		float distanceToClosestfood = Mathf.Infinity;
		food closestfood = null;
		food[] allfood = GameObject.FindObjectsOfType<food>();

		foreach (food currentfood in allfood)
		{
			float distanceTofood = (currentfood.transform.position - this.transform.position).sqrMagnitude;
			if (distanceTofood < distanceToClosestfood)
			{
				distanceToClosestfood = distanceTofood;
				closestfood = currentfood;
			}
		}

		if (distanceToClosestfood < distance)
		{
			Debug.DrawLine(this.transform.position, closestfood.transform.position);
			if (Input.GetKeyDown(KeyCode.E))
			{
				closestfood.GetComponent<food>().eat();
			}
		}
	}
}
