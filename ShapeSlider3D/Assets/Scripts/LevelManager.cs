using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public string[] levels;

	private int currentLevel = 0;
	private Scene currentScene;

	void Start()
	{
		//SceneManager.LoadSceneAsync(levels[0], LoadSceneMode.Additive);
	}
	public void AdvanceLevel()
	{
		LoadLevel(currentLevel + 1);
	}
	public void LoadInitialLevel()
	{
		LoadLevel(0);
	}
	private void LoadLevel(int level)
	{
		currentLevel = level;

		//Load next level in background
		string loadingScene = levels[level % levels.Length];
		SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
	}	
	private void DisableOldScene()
	{
		if (currentScene.IsValid())
		{
			//Disabled old scene
			GameObject[] oldSceneObjects = currentScene.GetRootGameObjects();
			for (int i = 0; i < oldSceneObjects.Length; i++)
			{
				oldSceneObjects[i].SetActive(false);
			}

			//Unload it
			SceneManager.UnloadSceneAsync(currentScene);
		}
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (mode != LoadSceneMode.Additive)
			return;

		SceneManager.SetActiveScene(scene);
		DisableOldScene();
		currentScene = scene;
	}
	void OnSceneUnloaded(Scene scene)
	{
		//SceneManager.SetActiveScene(scene);
		//DisableOldScene();
		//currentScene = scene;
	}
	public void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}
	public void QuitGame()
	{
		Application.Quit();
	}

}
