## 技术实现细节

### 难点1：角色翻转方案选型
问题：使用 Transform.Rotate 会导致 Animator 坐标系混乱，出现“左走播 Fall 动画”的 Bug
解决：改用 Transform.localScale = new Vector3(-1,1,1) 实现翻转，彻底解决问题

### 难点2：地面检测精度
问题：Mathf.Epsilon 阈值过小导致微抖动被误判为移动，角色状态被识别为“fall”
解决：设置 Mathf.Abs(velocity.x) > 0.1f 作为有效移动判定


## 项目结构
PlayerController.cs - 角色移动/跳跃/动画总控
PlayerHealth.cs - 生命值
Enemy.cs - 简单巡逻 AI
该项目中含有部分早起学习unity时解包《星露谷物语》产生的文件
