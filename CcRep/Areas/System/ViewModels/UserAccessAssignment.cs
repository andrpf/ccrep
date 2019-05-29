using CcRep.Helpers;
using CcRep.Models;
using CcRep.Models._dc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace CcRep.Areas.System.ViewModels
{
    public class UserAccessAssignment
    {
        public UserAccessAssignment()
        {

        }
        public UserAccessAssignment(CcRepUser user, CcRepContext db) : base()
        {
            Branches = MultiSelectHelper.GetUserFilialsList(user, db.Filials.ToList());
            Roles = MultiSelectHelper.GetUserRolesList(user, db.Roles.ToList());
            UserId = user.Id;
            UserFullName = user.FullName;
            UserName = user.UserName;
            Blocked = user.Locked;


            FlAccess = user.Claims.Where(c => c.ClaimType == "FlAccess")
                   .Select(c => c.ClaimValue).SingleOrDefault();

            PdAccess = user.Claims.Where(c => c.ClaimType == "PdAccess")
                   .Select(c => c.ClaimValue).SingleOrDefault();

            AllBranches = user.Claims.Where(c => c.ClaimType == "showAllBranches")
                   .Select(c => Convert.ToBoolean(c.ClaimValue.ToLower())).SingleOrDefault();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }

        public string[] SelectedBranches { get; set; }
        public MultiSelectList Branches { get; set; }

        public string[] SelectedRoles { get; set; }
        public MultiSelectList Roles { get; set; }

        [Display(Name = "Доступ к ФЛ")]
        public string FlAccess
        {
            get
            {
                return _FlAccess;
            }
            set
            {
                _FlAccess = Array.Exists(FlAccessArray, element => element == value) ? value : null;
            }
        }
        private string _FlAccess;

        [Display(Name = "Доступ к ПД")]
        public string PdAccess
        {
            get
            {
                return _PdAccess;
            }
            set
            {
                _PdAccess = Array.Exists(PdAccessArray, element => element == value) ? value : null;
            }
        }
        private string _PdAccess;

        [Display(Name = "Все филиалы")]
        public bool AllBranches { get; set; }

        [Display(Name = "Заблокировать")]
        public bool Blocked { get; set; }

        public static string[] PdAccessArray { get; } = { "Y", "*" };
        public static string[] FlAccessArray { get; } = { "ФЛ", "*" };

        public List<SelectListItem> PdSelectList
        {
            get
            {
                var pdAccList = new List<SelectListItem>();

                SelectListItem[] items = {
                    new SelectListItem() {Text = "ПД", Value = "Y" },
                    new SelectListItem() {Text = "*", Value = "*" }
                };

                pdAccList.AddRange(items);

                return pdAccList;
            }
        }

        public List<SelectListItem> FlSelectList
        {
            get
            {
                var flAccList = new List<SelectListItem>();

                SelectListItem[] items = {
                    new SelectListItem() {Text = "ФЛ", Value = "ФЛ" },
                    new SelectListItem() {Text = "*", Value = "*" }
                };

                flAccList.AddRange(items);

                return flAccList;
            }
        }
    }
}