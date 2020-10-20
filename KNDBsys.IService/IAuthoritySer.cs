using KNDBsys.IBLL;
using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNDBsys.IService
{
    public interface IAuthoritySer: ICrudService<Authority>
    {
        string GetAllAuthority();
        List<Authority> GetAllAuth();
        string GetUserAuth(string userid, string portType);
        List<Authority> GetAuthoritiesClz(string userid, string portType);
        string GetOperAuthByTag(string authtype, int tag);
    }

    public interface IDictionarySer : ICrudService<Sysdic>
    {
        string GetDicbytype(string type);
        List<Sysdic> GetDicByTypeName(string type);
        string UpdateDictionary(string dic);
        string DeleteSysdic(string dic);
    }

    public interface IUserInfoSer : ICrudService<RegUserInfo>
    {
        string GetAllUserInfo(string userid);
        string GetUserInfoByAccount(string account, string pwd);
        RegUserInfo GetUserInfobyID_claz(string userid);
        string GetUserInfobyID(string userid);
        RegUserInfo GetUserInfoByAccount_claz(string account, string pwd);
    }

    public interface IUserAuthSer : ICrudService<UserAuth>
    {
        string InsertTable(string authlistjson, string userid);
        string DeleteAuth(string userauthjson, string userid);
        string CopyAuth(string userid, string copyuserid);
    }

    public interface IServerTypeSer : ICrudService<ServerType>
    {
        string GetAllServerType(string userid);
    }

    public interface ICustomInfoSer : ICrudService<CustomInfo>
    {
        string GetAllCustomInfo(string userid);
        string FindSameCustomByTel(string tel);
        string FindCustomByTel(string tel);
        string FindCustomByid(string customid);
    }
    public interface ISysVerSer : ICrudService<SysVer>
    {
        string GetNewSysVer(string programtype);
    }
}
