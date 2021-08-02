using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject daojishi;
    public int TotalTime = 60;
    
    void Start()
    {
        
        StartCoroutine(Times());

    }

    
    IEnumerator Times()
    {
        while (TotalTime >= 0)
        {
            daojishi.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //daojishi.GetComponent<Text>().text = ;
        if (TotalTime == 0)
        {
            //baseS.RetrieveDataAndUpdate();
           
            /*TotalTime = 1;
            daojishi.GetComponent<Text>().text = TotalTime.ToString();*/
        }
    }
}
