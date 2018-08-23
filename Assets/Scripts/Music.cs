using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    // Use this for initialization

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        Invoke("LoadFirstScene", 2f);
        

    }

    void LoadFirstScene()
    {
       SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
