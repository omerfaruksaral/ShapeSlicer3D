using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float rayLength;
	[SerializeField]
	float rayOffsetX;
	[SerializeField]
	float rayOffsetY;
	[SerializeField]
	float rayOffsetZ;

	Vector3 xOffset;
	Vector3 yOffset;
	Vector3 zOffset;
	Vector3 zAxisOriginA;
	Vector3 zAxisOriginB;
	Vector3 xAxisOriginA;
	Vector3 xAxisOriginB;

	bool moving = true;

	void Start()
	{
		DOTween.Init();
		rayLength = 0.99f;
		rayOffsetX = transform.localScale.x/2.01f;
		rayOffsetY = transform.localScale.y/2.01f;
		rayOffsetZ = transform.localScale.z/2.01f;
	}
	void Update()
    {
		// Set the ray positions every frame

		yOffset = transform.position + Vector3.up * rayOffsetY;
		zOffset = Vector3.forward * rayOffsetZ;
		xOffset = Vector3.right * rayOffsetX;

		zAxisOriginA = yOffset + xOffset;
		zAxisOriginB = yOffset - xOffset;

		xAxisOriginA = yOffset + zOffset;
		xAxisOriginB = yOffset - zOffset;

		// Draw Debug Rays
		DebugRays();

		if ((MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.W)) && moving)
		{
			Move(Vector3.forward);

		}
		else if ((MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.S)) && moving)
		{
			Move(Vector3.back);
		}
		else if ((MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.A)) && moving)
		{
			Move(Vector3.left);
		}
		else if ((MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.D)) && moving)
		{
			Move(Vector3.right);
		}
	}
	
	private void DebugRays()
	{
		Debug.DrawLine(
						zAxisOriginA,
						zAxisOriginA + Vector3.forward * rayLength,
						Color.red,
						Time.deltaTime);
		Debug.DrawLine(
				zAxisOriginB,
				zAxisOriginB + Vector3.forward * rayLength,
				Color.red,
				Time.deltaTime);

		Debug.DrawLine(
				zAxisOriginA,
				zAxisOriginA + Vector3.back * rayLength,
				Color.red,
				Time.deltaTime);
		Debug.DrawLine(
				zAxisOriginB,
				zAxisOriginB + Vector3.back * rayLength,
				Color.red,
				Time.deltaTime);

		Debug.DrawLine(
				xAxisOriginA,
				xAxisOriginA + Vector3.left * rayLength,
				Color.red,
				Time.deltaTime);
		Debug.DrawLine(
				xAxisOriginB,
				xAxisOriginB + Vector3.left * rayLength,
				Color.red,
				Time.deltaTime);

		Debug.DrawLine(
				xAxisOriginA,
				xAxisOriginA + Vector3.right * rayLength,
				Color.red,
				Time.deltaTime);
		Debug.DrawLine(
				xAxisOriginB,
				xAxisOriginB + Vector3.right * rayLength,
				Color.red,
				Time.deltaTime);
	}
	private void Move(Vector3 direction)
	{
		moving = false;
		GameObject temp = new GameObject();
		temp.transform.position = transform.position;
		Transform temp2 = temp.transform;

		for (; ; )
		{
			if (CanMove(direction, temp2))
			{
				temp2.position += direction / 2;
			}
			else
				break;
		}
		transform.DOLocalMove(temp2.position, 0.5f).SetEase(Ease.OutQuint).OnComplete(() => moving = true);
		FindObjectOfType<AudioManager>().PlayAndDestroy("Move");
		Destroy(temp);
	}
	bool CanMove(Vector3 direction, Transform Cube)
	{
		if (Vector3.Equals(Vector3.forward,direction) || Vector3.Equals(Vector3.back, direction))
		{
			if (Physics.Raycast(Cube.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength)) return false;
			if (Physics.Raycast(Cube.position + Vector3.up * rayOffsetY + Vector3.left * rayOffsetX, direction, rayLength)) return false;
		}
		else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
		{
			if (Physics.Raycast(Cube.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
			if (Physics.Raycast(Cube.position + Vector3.up * rayOffsetY + Vector3.back * rayOffsetZ, direction, rayLength)) return false;
		}
		return true;
	}	
}
