using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyGaraOto.Models;
namespace QuanLyGaraOto.BUS
{
    public class UserBusinessLayer
    {
        public UserStatus GetUserStatus(string userName)
        {
            QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();
            UserDetail user = db.UserDetails.Find(userName.Trim());
            if (user != null)
            {
                if (user.UserTypeID == 1)
                    return UserStatus.AuthenticatedAdmin;
                else if (user.UserTypeID == 2)
                    return UserStatus.AuthenticatedUser;
                else
                    return UserStatus.NonAuthenticatedUser;
            }
            else
                return UserStatus.NonAuthenticatedUser;
        }
    }
}