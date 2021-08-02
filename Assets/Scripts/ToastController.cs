using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToastController : MonoBehaviour
{
    public GameObject openBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenToast()
    {
        //查找弹出框
        GameObject rankCanva = GameObject.Find("RankCanva").transform.Find("ToastImage").gameObject;
        
        GameObject buttonSelf = EventSystem.current.currentSelectedGameObject;
        string userName = buttonSelf.transform.Find("UserName").GetComponent<Text>().text.ToString();
        //Debug.Log(userName);
        rankCanva.transform.Find("ToastUserText").gameObject.GetComponent<Text>().text = "User:"+userName;
        string rankNum = buttonSelf.transform.Find("RankNum").GetComponent<Text>().text.ToString();

        rankCanva.transform.Find("ToastRankText").GetComponent<Text>().text ="Rank:"+rankNum;
        rankCanva.SetActive(true);

    }

    public void CloseToast()
    {
        GameObject rankCanva = GameObject.Find("RankCanva").transform.Find("ToastImage").gameObject;
        rankCanva.SetActive(false);
    }
}
