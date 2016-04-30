using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Models
{
    public class User_model
    {
    
        //用户信息
        public int userId;//编号
        public string realName;//真是姓名
        public string userName;//用户名
        public string touXiangUrl;//用户头像url
        public string password;//密码
        public string salt;//盐值
        public char sex;//性别    
        public int age;//年龄
        public DateTime birthDate;//出生年月
        public float height;//身高
        public float tuiChang;//腿长
        public float daTuiWei;//大腿围
        public float tunWei;//臀围
        public float yaoWei;//腰围
        public float biWei;//臂围
        public float xiongWei;//胸围
        public float xiaoTunWei;//小臀围
        public float weight;//体重
        public string skinColor;//肤色
        public string phoneNumber;//手机号码
        public string email;//邮箱
        public string signature;//个性签名
        public char rank;//用户等级或类型编号
        public string rankName;//用户等级或类型名
        public int starCount;//星星数
        public bool isMessageRemind;//是否开启信息提醒功能
        public int fansCount;//粉丝人数
        public int attentionCount;//关注人数
        public int postCount;//发帖条数
        public int videoCount;//上传视频数
        public int journalCount;//发表日志数
        public int collectPost;//收藏帖子数
        public int collectJournalCount;//收藏日志数
        public int collectVideoCount;//收藏视频数
        public int messageCount;//收到的消息条数
        public int inviteCount;//邀请次数
        public int beInvitedCount;//被邀请数

    }
}