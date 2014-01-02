using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CellPhoneService
{
    public class AccountDAO : DataDAO
    {
        public int CreateAccount(TaiKhoan tk)
        {
            if (IsUsernameExist(tk.TenDangNhap))
            {
                return RegisterState.UsernameExist;
            }
            try
            {
                Membership.CreateUser(tk.TenDangNhap, tk.MatKhau, tk.Email);
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                if (!Roles.RoleExists("User"))
                {
                    CreateRole("User");
                }
                Roles.AddUserToRole(tk.TenDangNhap, "User");
            }
            catch (Exception)
            {
                return RegisterState.Error;
            }

            return RegisterState.Success;
        }

        public TaiKhoan GetAccount(string username)
        {
            return db.TaiKhoans.Find(username);
        }

        public bool ChangePassword(UserMembershipModel user)
        {

            if (!Membership.ValidateUser(user.username, user.currentPassword))
            {
                return false;
            }
            MembershipUser mpuser = Membership.GetUser(user.username);
            if (!mpuser.ChangePassword(user.currentPassword, user.newPassword))
            {
                return false;
            }

            return true;
        }

        public string GetUsername(string email)
        {
            string result = "";
            MembershipUserCollection users = Membership.FindUsersByEmail(email);
            foreach (MembershipUser user in users)
            {
                result = user.UserName;
                break;
            }

            return result;
        }

        public bool UpdateAccount(TaiKhoan acc)
        {
            TaiKhoan tk = db.TaiKhoans.Find(acc.TenDangNhap);
            if (tk != null)
            {
                try
                {
                    db.Entry(tk).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public bool CheckAccount(LoginModel tk)
        {
            return Membership.ValidateUser(tk.TenDangNhap, tk.MatKhau);
        }

        public bool IsUsernameExist(string username)
        {
            return Membership.GetUser(username) != null;
        }

        public string[] GetRoles()
        {
            return Roles.GetAllRoles();
        }

        public string[] GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username);
        }

        public void SetRolesUser(string username, List<string> roles)
        {
            foreach (string role in Roles.GetAllRoles())
            {
                if (Roles.IsUserInRole(username, role))
                {
                    Roles.RemoveUserFromRole(username, role);
                }
            }
            foreach (string role in roles)
            {
                Roles.AddUserToRole(username, role);
            }
        }

        public bool CreateRole(string role)
        {
            if (Roles.RoleExists(role))
            {
                return false;
            }
            Roles.CreateRole(role);

            return true;
        }

        //public ListDetailsAccount GetAccounts(int pageIndex, int pageSize)
        //{
        //    ListDetailsAccount result = new ListDetailsAccount();
        //    int totalRecords;
        //    MembershipUserCollection list = Membership.GetAllUsers(pageIndex - 1, pageSize, out totalRecords);
        //    result.TotalAccount = totalRecords;
        //    if (list != null)
        //    {
        //        foreach (MembershipUser user in list)
        //        {
        //            AccountViewModel acc = new AccountViewModel();
        //            acc.Username = user.UserName;
        //            acc.DateCreate = string.Format("{0:dd/MM/yyyy}", user.CreationDate);
        //            acc.Email = user.Email;
        //            acc.Role = Roles.GetRolesForUser(user.UserName);
        //            result.ListAccount.Add(acc); ;
        //        }
        //    }

        //    return result;
        //}

        //public DetailsAccountViewModel GetDetailsAccount(string username)
        //{
        //    DetailsAccountViewModel result = new DetailsAccountViewModel();
        //    result.ThongTinTaiKhoan = GetAccountInMembership(username);
        //    result.ThongTinCaNhan = db.TaiKhoans.Find(username);

        //    return result;
        //}

        //private AccountViewModel GetAccountInMembership(string username)
        //{
        //    AccountViewModel result = new AccountViewModel();
        //    MembershipUser user = Membership.GetUser(username);
        //    result.Username = user.UserName;
        //    result.Username = user.UserName;
        //    result.DateCreate = string.Format("{0:dd/MM/yyyy}", user.CreationDate);
        //    result.Email = user.Email;
        //    result.Role = Roles.GetRolesForUser(user.UserName);

        //    return result;
        //}

        public bool DeleteRole(string role)
        {
            if (!Roles.RoleExists(role))
            {
                return false;
            }
            try
            {
                string[] usernames = Roles.GetUsersInRole(role);
                if (usernames != null && usernames.Length > 0)
                {
                    Roles.RemoveUsersFromRole(usernames, role);
                }
                Roles.DeleteRole(role);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}