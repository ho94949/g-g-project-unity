using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void UpdateRestart()
    {
        if (Input.GetAxisRaw("Restart") > 0.5)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateRestart();
 	}
}
