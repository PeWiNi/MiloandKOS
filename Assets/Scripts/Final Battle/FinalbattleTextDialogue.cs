using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalbattleTextDialogue : MonoBehaviour
{

    [SerializeField]
    Text
        kosPreBattleIfMilo;
    [SerializeField]
    Text
        miloPreBattleIfMilo;
    [SerializeField]
    Text
        kosPreBattleIfKOS;
    [SerializeField]
    Text
        miloPreBattleIfKOS;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	    
    }

    void StartDialogues()
    {
        if (Application.loadedLevel == 7)//As Milo.
        {

        } else if (Application.loadedLevel == 8)//As KOS.
        {

        }
    }
}
