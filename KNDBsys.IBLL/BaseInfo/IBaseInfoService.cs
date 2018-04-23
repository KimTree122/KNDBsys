using KNDBsys.Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.IBLL.BaseInfo
{
    class IBaseInfoService
    {
    }

    public interface IAuthorityService : IBaseServiceBLL<Authority>
    {

    }
    public interface IUserAuthService : IBaseServiceBLL<UserAuth>
    {

    }
    public interface ICSDicTionaryService : IBaseServiceBLL<CSDicTionary>
    {

    }
}
