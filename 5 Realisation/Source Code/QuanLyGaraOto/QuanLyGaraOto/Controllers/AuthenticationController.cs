﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuanLyGaraOto.BUS;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                UserBusinessLayer userBusiness = new UserBusinessLayer();
                var userName = form["UserName"].ToString();
                var passWord = form["Password"].ToString();
                UserStatus userStatus  =  userBusiness.GetUserStatus(userName);
                bool IsAdmin = false;
                if (userStatus == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (userStatus == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "User name hoặc Password không hợp lệ.");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(userName, false);
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    return RedirectToAction("Index", "UserDetails");
                }
                else
                {
                    return RedirectToAction("Index", "PhieuTiepNhans");
                }
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    } 
}