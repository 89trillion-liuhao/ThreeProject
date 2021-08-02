using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openOrCloseRank()
    {
        
        GameObject buttonSelf = EventSystem.current.currentSelectedGameObject;
        Text openValue = buttonSelf.transform.Find("Text").GetComponent<Text>();
        
        
        
        GameObject rank = GameObject.Find("RankCanvas").transform.Find("OSA").gameObject;
        if (rank.activeSelf)
        {
            rank.SetActive(false);
            openValue.text = "打开排行榜";
        }
        else
        {
            rank.SetActive(true);
            openValue.text = "关闭排行榜";
        }
       
    }
    
}
