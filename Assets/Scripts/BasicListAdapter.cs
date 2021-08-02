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
using SimpleJSON;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
	// There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
	// See explanations below
	
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		// Helper that stores data and notifies the adapter when items count changes
		// Can be iterated and can also have its elements accessed by the [] operator
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		public int totalTime = 60;
		public int hour = 23;
		public int minutes = 59;
		public GameObject daojishi;
		
		public TextAsset simple;
		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);

			// Calling this initializes internal data and prepares the adapter to handle item count changes
			base.Awake();
			StartCoroutine(Times());
			// Retrieve the models from your data source and set the items count
			
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
		
		
		
		
		
		
		// This is called initially, as many times as needed to fill the viewport, 
		// and anytime the viewport's size grows, thus allowing more items to be displayed
		// Here you create the "ViewsHolder" instance whose views will be re-used
		// *For the method's full description check the base implementation
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();

			// Using this shortcut spares you from:
			// - instantiating the prefab yourself
			// - enabling the instance game object
			// - setting its index 
			// - calling its CollectViews()
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
			
			return instance;
		}

		// This is called anytime a previously invisible item become visible, or after it's created, 
		// or when anything that requires a refresh happens
		// Here you bind the data from the model to the item's views
		// *For the method's full description check the base implementation
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{
			// In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
			// index of item that should be represented by this views holder. You'll use this index
			// to retrieve the model from your data set
			MyListItemModel model = Data[newOrRecycled.ItemIndex];

			int thisIndex = model.thisNum;
			//设置奖杯图片
			Sprite rankImage_1 = Resources.Load("UI/icon_rank_1", typeof(Sprite)) as Sprite;
			Sprite rankImage_2 = Resources.Load("UI/icon_rank_2", typeof(Sprite)) as Sprite;
			Sprite rankImage_3 = Resources.Load("UI/icon_rank_3", typeof(Sprite)) as Sprite;
			GameObject BannerMyName = GameObject.Find("RankCanva/Banner/BannerMyName");
			GameObject myCupNum = GameObject.Find("RankCanva/Banner/MyCupNum");
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
				
				//newOrRecycled.rankNumIco.sprite = null;
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
				
			
			
			
			
			
			
			//newOrRecycled.backgroundImage.color = model.color;
			//newOrRecycled.titleText.text = model.title + " #" + newOrRecycled.ItemIndex;
		}

		// This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
		// especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
		// download request, if it's still in progress when the item goes out of the viewport.
		// <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
		// *For the method's full description check the base implementation
		/*
		protected override void OnBeforeRecycleOrDisableViewsHolder(MyListItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
		{
			base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
		}
		*/

		// You only need to care about this if changing the item count by other means than ResetItems, 
		// case in which the existing items will not be re-created, but only their indices will change.
		// Even if you do this, you may still not need it if your item's views don't depend on the physical position 
		// in the content, but they depend exclusively to the data inside the model (this is the most common scenario).
		// In this particular case, we want the item's index to be displayed and also to not be stored inside the model,
		// so we update its title when its index changes. At this point, the Data list is already updated and 
		// shiftedViewsHolder.ItemIndex was correctly shifted so you can use it to retrieve the associated model
		// Also check the base implementation for complementary info
		/*
		protected override void OnItemIndexChangedDueInsertOrRemove(MyListItemViewsHolder shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
		{
			base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);

			shiftedViewsHolder.titleText.text = Data[shiftedViewsHolder.ItemIndex].title + " #" + shiftedViewsHolder.ItemIndex;
		}
		*/
		#endregion

		// These are common data manipulation methods
		// The list containing the models is managed by you. The adapter only manages the items' sizes and the count
		// The adapter needs to be notified of any change that occurs in the data list. Methods for each
		// case are provided: Refresh, ResetItems, InsertItems, RemoveItems
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
		public  void RetrieveDataAndUpdate()
 		{
	        Debug.Log("更新数据");
			//StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate());
		}

		// Retrieving <count> models from the data source and calling OnDataRetrieved after.
		// In a real case scenario, you'd query your server, your database or whatever is your data source and call OnDataRetrieved after
		// 读取并解析json
		//IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate()
		{
			// Simulating data retrieving delay
			yield return new WaitForSeconds(.5f);
			
			
			//读取并解析json
			simple = Resources.Load("ranklist") as TextAsset;
			JSONNode tempJsons = JSON.Parse(simple.text);
			int count = tempJsons["list"].Count;
			
			//生成对象
			
			
			var newItems = new MyListItemModel[count];
			//public string uid;
			// public string nickName;
			// public int avatar;
			// public int trophy;
			// public string thirdAvatar;
			// public int onlineStatus;
			// public int role;
			// public string abb;
			// Retrieve your data here
			
			
			
			
			
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
			OnDataRetrieved(newItems);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			Data.InsertItemsAtEnd(newItems);
			
		}
	}

	// Class containing the data associated with an item
	public class MyListItemModel
	{
	
		public string uid;
		public string nickName;
		public int avatar;
		public int trophy;
		public string thirdAvatar;
		public int onlineStatus;
		public int role;
		public string abb;
		public int thisNum;


	}


	// This class keeps references to an item's views.
	// Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
	public class MyListItemViewsHolder : BaseItemViewsHolder
	{
		public Text titleText;
		public Image backgroundImage;
		public Image rankDetailBg;
		public Image arenaBad;
		public Text rankNum;
		public Image rankNumIco;
		public Text rankNumSort;
		public Text userName;

		// Retrieving the views from the item's root GameObject
		public override void CollectViews()
		{
			base.CollectViews();

			// GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
			// which infers the variable's component from its type, so you won't need to specify it yourself
			root.GetComponentAtPath("TitleText", out titleText);
			root.GetComponentAtPath("BackgroundImage", out backgroundImage);
			root.GetComponentAtPath("RankDetailBg", out rankDetailBg);
			root.GetComponentAtPath("RankNum", out rankNum);
			root.GetComponentAtPath("RankNumIco", out rankNumIco);
			root.GetComponentAtPath("RankNumSort", out rankNumSort);
			root.GetComponentAtPath("UserName", out userName);
			root.GetComponentAtPath("ArenaBad", out arenaBad);
		
		}

		// Override this if you have children layout groups or a ContentSizeFitter on root that you'll use. 
		// They need to be marked for rebuild when this callback is fired
		/*
		public override void MarkForRebuild()
		{
			base.MarkForRebuild();

			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout1);
			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout2);
			YourSizeFitterOnRoot.enabled = true;
		}
		*/

		// Override this if you've also overridden MarkForRebuild() and you have enabled size fitters there (like a ContentSizeFitter)
		/*
		public override void UnmarkForRebuild()
		{
			YourSizeFitterOnRoot.enabled = false;

			base.UnmarkForRebuild();
		}
		*/
	}
}
