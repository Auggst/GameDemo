# RPGDemo
## 角色
移动：采用Unity的导航网格功能实现了鼠标点击移动，同时武器决定攻击范围，如果不在攻击范围内会先移动到攻击范围内再攻击

UI：全部使用UGUI制作，角色的血条，能量条，技能。
![image](https://github.com/Auggst/GameDemo/blob/master/UI.png)

攻击：实现了武器系统，技能系统，以及暴击效果。
![image](https://github.com/Auggst/GameDemo/blob/master/%E6%9C%A8%E5%89%91%E6%AD%A6%E5%99%A8.png)
![image](https://github.com/Auggst/GameDemo/blob/master/%E6%AD%A6%E5%99%A8%E7%B3%BB%E7%BB%9F.png)

##敌人
状态:设置有四个状态：静止，巡逻，追击，攻击
静止状态
![image](https://github.com/Auggst/GameDemo/blob/master/%E9%9D%99%E6%AD%A2%E7%8A%B6%E6%80%81.png)
巡逻状态
![image](https://github.com/Auggst/GameDemo/blob/master/%E5%B7%A1%E9%80%BB%E7%8A%B6%E6%80%81.png)
攻击状态
![image](https://github.com/Auggst/GameDemo/blob/master/%E6%94%BB%E5%87%BB%E7%8A%B6%E6%80%81.png)

##NPC
目前只设置了触发事件，并未添加任务系统，于12末考试，所以暂且先搁置，等考完试在添加。目前想法是使用UGUI实现任务系统。

##场景
地形使用Unity的地形系统自己制作，建模采用网络资源，不进行商用，只为做个Demo，学习。

