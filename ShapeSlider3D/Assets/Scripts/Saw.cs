using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		Cube.Cut(collision.transform, transform.position);
	}
}
