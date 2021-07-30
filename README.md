# ThreeProject
排行榜
## 整体框架
1、根据需求分析项目需求  
2、完成技术文档  
3、根据技术文档设计界面  
4、学习OSA插件  
5、编写函数并给页面绑定事件  
6、运行调试代码  
## 目录结构
Project视图
> Assest
>> Scene
>> Rescourses
>>> Prefab  
>>>> ItemShow
>>> UI  
## 代码结构
>> Script 
>>> Controller
>>>> MainSceneController.cs 主控制器  
>>>>> OpenOrCloseRank 开启/关闭排行榜
>>>>> RankCTRLBtnClick 开启关闭按钮点击事件
>>>>> CreateRankPlane 使用代码生成画布  
>>>>> CreateRankDetails 生成具体某个rank信息
>>>>> OpenRankDetails 点击某个滑块展示rank具体信息
>>>>> GetJsonCreateShowItem 解析json并实例化相应对象
>>>>> UseOSAMethod 使用OSA插件. 
>>>>> TimeReset 1秒刷新文本
>>> Pojos
>>>> ShowItem.cs 展示的每个滑块对象
>>> Utils 第三方工具包
>>>> SimpleJson.cs 
>>>> OSA.cs……
## 层级视图
MainScene  
> RootCanvas
>> TopPanel
>>> TopImage
>>> TopBanner
>>> TopText
>> CenterCanvas
>>> Scroll View
>> BottomPanel
>>> BottomImage 
>>> BottomBtn

## 流程图
![Image text](https://github.com/89trillion-liuhao/myTest/blob/main/1.png)
