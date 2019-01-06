﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.DAL.Entities.Model;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IUserService : IService<User>
    {
        User ValidateUser(string userName, string password);
        bool CheckUserNameDuplicated(string userName, long userId);
        User CheckUserIsDeleted(string userName, string password);
    }
}
