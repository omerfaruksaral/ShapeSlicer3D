using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
	private LevelManager levelM;
	bool load = false;

	public Slider slider;
	private float targetProgress = 0f;
	
	public float cubeCount = 1f;

	void Start()
	{		
		levelM = GetComponent<LevelManager>();
		slider.value = 0;
		slider.maxValue = cubeCount;
	}
	void Update()
	{
		if (slider.value < targetProgress)
		{
			slider.value = targetProgress;
		}

		if (slider.value == cubeCount && !load)
		{
			FindObjectOfType<AudioManager>().Destroy("Up");
			FindObjectOfType<AudioManager>().PlayAndDestroy("NextLevel");
			levelM.AdvanceLevel();
			load = true;
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		int area = (int)(other.transform.localScale.x * other.transform.localScale.z);
		if (area == 1)
		{
			FindObjectOfType<AudioManager>().PlayAndDestroy("Up");
			Destroy(other.gameObject);
			targetProgress = slider.value + 1;
		}		
	}
}
