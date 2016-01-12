using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPM.Utility
{
    public class Keys
    {
        /// <summary>
        /// SESSION键
        /// </summary>
        public enum SessionKeys
        {
            /// <summary>
            /// 登录用户ID
            /// </summary>
            UserID,
            /// <summary>
            /// 用户登录唯一ID
            /// </summary>
            UserUniqueID,
            /// <summary>
            /// 登录用户
            /// </summary>
            Users,
            /// <summary>
            /// 验证码
            /// </summary>
            ValidateCode,
            /// <summary>
            /// 是否要检查验证码
            /// </summary>
            IsValidateCode,
            /// <summary>
            /// 主题
            /// </summary>
            Theme,
            /// <summary>
            /// 根路径
            /// </summary>
            BaseUrl,
            /// <summary>
            /// 用户登陆相关信息
            /// </summary>
            CurrentLoginData,
            /// <summary>
            /// 用户明细数据连接字符串
            /// </summary>
            DBConnectString
        }

        /// <summary>
        /// 缓存键
        /// </summary>
        public enum CacheKeys
        {
            /// <summary>
            /// 角色所有应用
            /// </summary>
            RoleApp,
            /// <summary>
            /// 应用程序库
            /// </summary>
            AppLibrary,
            /// <summary>
            /// 工作流按钮
            /// </summary>
            WorkFlowButtons,
            /// <summary>
            /// 数据库连接
            /// </summary>
            DBConnnections,
            /// <summary>
            /// 流程已安装实体(后跟流程ID)
            /// </summary>
            WorkFlowInstalled_,
            /// <summary>
            /// 系统类实例(后跟类名)
            /// </summary>
            ClassInstance_,
            /// <summary>
            /// 流程意见
            /// </summary>
            WorkFlowComments,
            /// <summary>
            /// 在线用户
            /// </summary>
            OnlineUsers,
            /// <summary>
            /// 工作流委托
            /// </summary>
            WorkFlowDelegation,
            /// <summary>
            /// 用户角色
            /// </summary>
            UserRoles,
            /// <summary>
            /// 数据字典
            /// </summary>
            Dictionary
        }
    }
}
