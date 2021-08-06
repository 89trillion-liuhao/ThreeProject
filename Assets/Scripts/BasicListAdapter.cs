/*
 * * * * This bare-bones script was auto-generated * * * *
 * The code commented with "/ * * /" demonstrates how data is retrieved and passed to the adapter, plus other common commands. You can remove/replace it once you've got the idea
 * Complete it according to your specific use-case
 * Consult the Example scripts if you get stuck, as they provide solutions to most common scenarios
 * 
 * Main terms to understand:
 *		Model = class that contains the data associated with an item (title, content, icon etc.)
 *		Views Holder = class that contains references to your views (Text, Image, MonoBehavior, etc.)
 * 
 * Default expected UI hiererchy:
 *	  ...
 *		-Canvas
 *		  ...
 *			-MyScrollViewAdapter
 *				-Viewport
 *					-Content
 *				-Scrollbar (Optional)
 *				-ItemPrefab (Optional)
 * 
 * Note: If using Visual Studio and opening generated scripts for the first time, sometimes Intellisense (autocompletion)
 * won't work. This is a well-known bug and the solution is here: https://developercommunity.visualstudio.com/content/problem/130597/unity-intellisense-not-working-after-creating-new-1.html (or google "unity intellisense not working new script")
 * 
 * 
 * Please read the manual under "Assets/OSA/Docs", as it contains everything you need to know in order to get started, including FAQ
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using DefaultNamespace;
using SimpleJSON;

// 生成滑动列表框的主要方法
namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
	
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		public int totalTime = 60;
		public int hour = 23;
		public int minutes = 59;
		public GameObject daojishi;
		//用来读取json文件的字段
		public TextAsset simple;
		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);
			base.Awake();
			//倒计时功能
			StartCoroutine(Times());

		}

		
		IEnumerator Times()
		{
			while (totalTime >= 0)
			{
				daojishi.GetComponent<Text>().text = hour.ToString()+":"+minutes.ToString()+":"+ totalTime.ToString();
				yield return new WaitForSeconds(1);
				//重置数据
				Data = new SimpleDataHelper<MyListItemModel>(this);
				RetrieveDataAndUpdate();
				if (totalTime == 0)
				{
					if (minutes == 0)
					{
						hour--;
						minutes = 60;
					}
					minutes--;
					totalTime = 60;

				}
				totalTime--;
			}
        
		}
		
		
		/**
		 * 该方法是创建时的方法
		 */
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
			
			return instance;
		}

		
		/**
		 * 该方法是更新视图时调用的方法
		 */
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{
			// In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
			// index of item that should be represented by this views holder. You'll use this index
			// to retrieve the model from your data set
			MyListItemModel model = Data[newOrRecycled.ItemIndex];

			int thisIndex = model.thisNum;
			//设置奖杯图片sprite
			Sprite rankImage_1 = Resources.Load("UI/icon_rank_1", typeof(Sprite)) as Sprite;
			Sprite rankImage_2 = Resources.Load("UI/icon_rank_2", typeof(Sprite)) as Sprite;
			Sprite rankImage_3 = Resources.Load("UI/icon_rank_3", typeof(Sprite)) as Sprite;
			GameObject BannerMyName = GameObject.Find("RankCanva/Banner/BannerMyName");
			GameObject myCupNum = GameObject.Find("RankCanva/Banner/MyCupNum");
			
			//设置前123排名图片   如果是1 2 3 则 排名框不透明度设置为 1  否则设置为0 显示
			if (model.thisNum == 1)
			{
				BannerMyName.GetComponent<Text>().text = model.nickName;
				myCupNum.GetComponent<Text>().text = model.trophy.ToString();
				newOrRecycled.rankNumIco.sprite = rankImage_1;
				newOrRecycled.rankNumIco.color = new Color(1,1,1,1);
			}else if ( model.thisNum == 2)
			{
				newOrRecycled.rankNumIco.sprite = rankImage_2;
				newOrRecycled.rankNumIco.color = new Color(1,1,1,1);
			}
			
			else if (model.thisNum == 3)
			{
				newOrRecycled.rankNumIco.sprite = rankImage_3;
				newOrRecycled.rankNumIco.color = new Color(1,1,1,1);
			}
			else
			{	
				newOrRecycled.rankNumIco.color = new Color(1,1,1,0);
			}
			newOrRecycled.rankNum.text = model.thisNum.ToString();
			//newOrRecycled.rankNum.text = model.thisNum.ToString();
			//设置奖杯数
			newOrRecycled.rankNumSort.text = model.trophy.ToString();
			//设置用户名
			newOrRecycled.userName.text = model.nickName;
			//设置段位
			
			int name = model.trophy / 1000 +1;
			newOrRecycled.arenaBad.sprite = Resources.Load("UI/Rank/arenaBadge_"+name,typeof(Sprite)) as Sprite;
		}

		
		/*
		protected override void OnBeforeRecycleOrDisableViewsHolder(MyListItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
		{
			base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
		}
		*/

		
		/*
		protected override void OnItemIndexChangedDueInsertOrRemove(MyListItemViewsHolder shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
		{
			base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);

			shiftedViewsHolder.titleText.text = Data[shiftedViewsHolder.ItemIndex].title + " #" + shiftedViewsHolder.ItemIndex;
		}
		*/
		#endregion
		
		#region data manipulation
		public void AddItemsAt(int index, IList<MyListItemModel> items)
		{
			// Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.InsertRange(index, items);
			//InsertItems(index, items.Length);

			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			// Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.RemoveRange(index, count);
			//RemoveItems(index, count);

			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<MyListItemModel> items)
		{
			// Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
			//YourList.Clear();
			//YourList.AddRange(items);
			//ResetItems(YourList.Count);

			Data.ResetItems(items);
		}
		#endregion


		// Here, we're requesting <count> items from the data source
		// 倒计时功能每秒刷新一次
		public  void RetrieveDataAndUpdate()
 		{
	        //StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate());
		}

		// 读取并解析json
		//IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate()
		{
			yield return new WaitForSeconds(.5f);
			
			
			//读取并解析json
			simple = Resources.Load("ranklist") as TextAsset;
			JSONNode tempJsons = JSON.Parse(simple.text);
			int count = tempJsons["list"].Count;
			//生成对象   
			var newItems = new MyListItemModel[count];
			for (int i = 0; i < count; ++i)
			{
				var tempOneJson = tempJsons["list"][i];
				var model = new MyListItemModel()
				{
					uid = tempOneJson["uid"],
					nickName = tempOneJson["nickName"],
					avatar = tempOneJson["avatar"],
					trophy = tempOneJson["trophy"],
					thirdAvatar = tempOneJson["thirdAvatar"],
					onlineStatus = tempOneJson["onlineStatus"],
					role = tempOneJson["onlineStatus"],
					abb = tempOneJson["onlineStatus"],
				};
				newItems[i] = model;
			}
			//对数据进行排序
			for (int i = 0; i < count ; i++)
			{
				for ( int j = 0 ;j < i;j++)
				{
					if (newItems[i].trophy > newItems[j].trophy)
					{
						var tempItem = newItems[j];
						newItems[j] = newItems[i];
						newItems[i] = tempItem;
					}
				}
			}
			//角标赋值
			for (int i = 0 ; i < count ; i++)
			{
				newItems[i].thisNum = i + 1;
			}
			//插入数据
			OnDataRetrieved(newItems);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			//插入数据
			Data.InsertItemsAtEnd(newItems);
			
		}
	}
	
	

}
