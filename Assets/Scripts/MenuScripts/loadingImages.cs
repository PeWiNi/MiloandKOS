using UnityEngine;
using System.Collections;

public class loadingImages : MonoBehaviour
{
    Texture2D[] imageList;
    public string nameOfCutscene;
    public int numberOFFrameInCutscene; 
    public int currentIndex;
    public	string returnString;
    public GameObject planeCut;
    public string nextScene;

    Vector3 v3ViewPort;
    Vector3 v3BottomLeft;
    Vector3 v3TopRight;

    void Start()
    {
        imageList = new Texture2D[numberOFFrameInCutscene + 1];
        currentIndex = 0;
        planeCut = GameObject.Find("Plane");

        Texture2D tempTex = Resources.Load<Texture2D>(toStringg(currentIndex));
        imageList [currentIndex] = tempTex;
        planeCut.renderer.material.mainTexture = imageList [currentIndex];

        StartCoroutine(loadNextImage());
    }

    // Update is called once per frame
    void Update()
    {
        updateScreenSize();
        if (currentIndex < numberOFFrameInCutscene)
        {	
            //Debug.Log(Time.time);
        } else  
			if (nextScene.Equals("none"))
            Application.Quit();
        else 
            Application.LoadLevel(nextScene);
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(nextScene);
    }

    public string toStringg(int num)
    {	
        returnString = nameOfCutscene + "/" + nameOfCutscene;
        int zeros = 3;
        int tempNum = num;
        while (num/10>0)
        {
            zeros--;
            num /= 10;
        }
        for (int i=0; i<zeros; i++)
        {
            int n = 0;
            returnString += n.ToString();
        }
        returnString += tempNum.ToString();
        return returnString;
    }

    IEnumerator loadNextImage()
    {
        while (currentIndex < numberOFFrameInCutscene)
        {
            yield return new WaitForSeconds(0.03f);
            Texture2D tempTex = Resources.Load<Texture2D>(toStringg(currentIndex));
            imageList [currentIndex] = tempTex;
            planeCut.renderer.material.mainTexture = imageList [currentIndex];	
            currentIndex++;
        }
    }

    void updateScreenSize()
    {
        v3ViewPort.Set(0, 0, -1);
        v3BottomLeft = Camera.main.ViewportToWorldPoint(v3ViewPort);
        v3ViewPort.Set(1, 1, -1);
        v3TopRight = Camera.main.ViewportToWorldPoint(v3ViewPort);
        float ratiow = 360 * (v3BottomLeft.x - v3TopRight.x) / 540;
        float ratioh = 540 * (v3BottomLeft.y - v3TopRight.y) / 360;
        planeCut.transform.localScale = new Vector3(ratiow, 1, ratioh - 0.5f);
    }
}
