using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
    void UpdateRestart()
    {
        if (Input.GetAxisRaw("Restart") > 0.5)
            Restart();
    }

    private List<Part> savedPartList = new List<Part>();
    private List<GameObject> savedGameObjectList = new List<GameObject>();
    private List<Part> disabledSavedPartList = new List<Part>();
    private List<GameObject> disabledSavedGameObjectList = new List<GameObject>();
    // Update is called once per frame
    void partUpdate()
    {
        int cnt = 0;
        List<Part> partList = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<MoveitMoveit>().partList;
        foreach (Part x in partList)
            foreach (Part y in savedPartList)
            {
                if (x == y) cnt++;
            }
        if (cnt == partList.Count && cnt == savedPartList.Count)
            return;
        foreach (GameObject x in savedGameObjectList)
        {
            Destroy(x);
        }
        savedGameObjectList = new List<GameObject>();
        savedPartList = new List<Part>();
        float xc = -10.3f;
        float yc = 4.3f;
        foreach (Part x in partList)
        {
            xc += 1.2f;
            savedPartList.Add(x);
            GameObject objToSpawn = new GameObject(x.Name());
            objToSpawn.transform.position = new Vector2(xc, yc);
            objToSpawn.AddComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>(x.Name());
            objToSpawn.GetComponent<SpriteRenderer>().sprite = sprite;
            objToSpawn.GetComponent<SpriteRenderer>().sortingLayerName = "Information";
            savedGameObjectList.Add(objToSpawn);
        }
    }
    void disabledPartUpdate()
    {
        int cnt = 0;
        List<Part> disabledPartList = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<MoveitMoveit>().disabledPartList;
        foreach (Part x in disabledPartList)
            foreach (Part y in disabledSavedPartList)
            {
                if (x == y) cnt++;
            }
        if (cnt == disabledPartList.Count && cnt == disabledSavedPartList.Count)
            return;
        foreach (GameObject x in disabledSavedGameObjectList)
        {
            Destroy(x);
        }
        disabledSavedGameObjectList = new List<GameObject>();
        disabledSavedPartList = new List<Part>();
        float xc = 10.3f;
        float yc = 4.3f;
        foreach (Part x in disabledPartList)
        {
            xc -= 1.2f;
            disabledSavedPartList.Add(x);
            GameObject objToSpawn = new GameObject(x.Name());
            objToSpawn.transform.position = new Vector2(xc, yc);
            objToSpawn.AddComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>(x.Name());
            objToSpawn.GetComponent<SpriteRenderer>().sprite = sprite;
            objToSpawn.AddComponent<DisplayPart>();
            objToSpawn.AddComponent<BoxCollider2D>();
            objToSpawn.GetComponent<BoxCollider2D>().isTrigger = true;
            objToSpawn.GetComponent<SpriteRenderer>().sortingLayerName = "Information";
            disabledSavedGameObjectList.Add(objToSpawn);
        }
    }
    void Update () {
        UpdateRestart();
        //Fucking Fucking Show Parts Fucking Fucking Parts Fuck
        //Oh Yeah Fucking Lets Find Fucking Fuicking P{Arts
        //oh yea fucking fucking displ;ay fucking fucking parts 
        partUpdate();

        disabledPartUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Test");
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hit = Physics2D.RaycastAll(worldPoint, Vector2.zero);
            foreach (RaycastHit2D r in hit)
            {
                Debug.Log(r.collider);
                if (r.collider.GetComponent<DisplayPart>() != null)
                {
                    
                    GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<MoveitMoveit>().Addpart(PartManager.getPart(r.collider.name));
                }

            }
        }
    }
}
