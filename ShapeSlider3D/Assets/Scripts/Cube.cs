using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public static bool Cut(Transform victim, Vector3 _pos)
	{
		Vector3 pos = new Vector3(_pos.x, 0.25f, (int)victim.position.z);//(_pos.x, victim.position.y, victim.position.z)
		Vector3 victimScale = victim.localScale;
		float distance = Vector3.Distance(victim.position, pos);
		if (distance >= victimScale.x / 2) return false;		
		
		Vector3 leftPoint = victim.position - Vector3.right * victimScale.x / 2;
		Vector3 rightPoint = victim.position + Vector3.right * victimScale.x / 2;
		Material mat = victim.GetComponent<MeshRenderer>().material;
		Destroy(victim.gameObject);		

		GameObject rightSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		rightSideObj.transform.position = new Vector3(rightPoint.x / 2 + pos.x, rightPoint.y / 2 + pos.y, (int)(rightPoint.z / 2 + pos.z));   //(rightPoint / 2 + pos);
		float rightWidth = Vector3.Distance(pos, rightPoint);
		if (rightWidth < 1) rightWidth = 1;
		rightSideObj.transform.localScale = new Vector3((int)rightWidth, (int)victimScale.y, (int)victimScale.z);
		rightSideObj.AddComponent<Rigidbody>();
		rightSideObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		rightSideObj.GetComponent<MeshRenderer>().material = mat;
		rightSideObj.AddComponent<PlayerMovement>();
		rightSideObj.AddComponent<MobileInput>();		

		GameObject leftSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		leftSideObj.transform.position = new Vector3(leftPoint.x / 2 + pos.x, leftPoint.y / 2 + pos.y, (int)(leftPoint.z / 2 + pos.z));
		float leftWidth = Vector3.Distance(pos, leftPoint);
		if (leftWidth < 1) leftWidth = 1;
		leftSideObj.transform.localScale = new Vector3((int)leftWidth, (int)victimScale.y, (int)victimScale.z);
		
		leftSideObj.AddComponent<Rigidbody>();
		leftSideObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		leftSideObj.GetComponent<MeshRenderer>().material = mat;
		leftSideObj.AddComponent<PlayerMovement>();
		leftSideObj.AddComponent<MobileInput>();

		return true;
	}
}
