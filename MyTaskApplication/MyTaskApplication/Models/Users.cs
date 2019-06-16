using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTaskApplication.Models
{
    //Register form business class model
    public class RegisterModel
    {
        //server-side validation
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([0-9a-zA-Z](\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        //server-side validation
        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength =6)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{6,}",ErrorMessage = "Your password must be at least 6 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    //Login form business class model
    public class LoginModel
    {

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        // Validate login and password from database. 
        //I used ADO.NET database connection 

        public bool IsValid(string _username, string _password)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(constr);
            {
                string _sql = @"SELECT [UserName] FROM [dbo].[Users] " +
                       @"WHERE [UserName] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _password;
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}
