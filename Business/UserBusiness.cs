using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Util;
using System.Web;
namespace Business
{
    public class UserBusiness:BaseBusiness<User>
    {
        /// <summary>
        /// 获取所有用户，仅供测试用
        /// </summary>
        /// <returns></returns>
        public string GetUserList()
        {
            return GetList().ToJson();
        }


        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="newUser">账号信息</param>
        /// <returns></returns>
        public string Register(User newUser)
        {
            newUser.Id = Guid.NewGuid().ToString();
            if(GetTable().Where(x=>x.Account==newUser.Account).Count()==0)
            {
                Insert(newUser);
                var res = new
                {
                    State = 1,
                    Msg="注册成功！"
                };
                return res.ToJson();
            }
            else
            {
                var res = new
                {
                    State = -1,
                    Msg = "此账号已注册！"
                };
                return res.ToJson();
            }
        }

        /// <summary>
        /// 账号登陆
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string account,string password)
        {
            if(GetTable().Where(x=>x.Account==account&&x.Password==password).Count()==0)
            {
                return -1;
            }
            else
            {            
                return 1;
            }
        }
    }
}
