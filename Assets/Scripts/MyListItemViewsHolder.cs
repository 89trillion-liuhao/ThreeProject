using Com.TheFallenGames.OSA.Core;
using UnityEngine.UI;

using frame8.Logic.Misc.Other.Extensions;


namespace DefaultNamespace
{
    /**
     * unity页面展示的实体类
     * 
     */
    public class MyListItemViewsHolder :BaseItemViewsHolder
    {
        public Text titleText; // 标题文字
        public Image backgroundImage; //
        public Image rankDetailBg;//背景图片
        public Image arenaBad;//
        public Text rankNum;//排名
        public Image rankNumIco;//图标
        public Text rankNumSort;//奖杯数
        public Text userName;//玩家用户名

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