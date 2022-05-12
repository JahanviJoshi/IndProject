using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TicketGenerator.Models;
using TicketGenerator.Services;

namespace TicketGenerator.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService<Login, int> _loginService;
        private readonly TickectCreatingDbContext ctx;
        public LoginController(IService<Login, int> _loginService, TickectCreatingDbContext ctx)
        {
            this._loginService = _loginService;
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUser()
        {
            Login user = new Login();
            return View(user);
        }



        [HttpPost]
        public async Task<IActionResult> GetUser(Login user)
        {

            try
            {

                var LoginCheck = ctx.Logins.ToList(); //_loginService.Get().Result;
                
                var res = LoginCheck.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
                if (res == null)
                {
                    ViewBag.Message = "Wrong Email or Password";
                    return View(res);
                }
                var passdecrypt = DecryptAsync(res.PassWord);
                if(user.PassWord == passdecrypt)
                {
                    HttpContext.Session.SetInt32("UserId", res.Uid);
                    HttpContext.Session.SetInt32("RoleId", res.RoleId);
                    HttpContext.Session.SetString("EmailId", res.EmailId);
                    user.SignIn = DateTime.Now;
                    //return View(user);
                   // HttpContext.Session.SetInt32("RoleId", res.RoleId);
                    return RedirectToAction("Create", "TicketGenrating", new { UserId = res.Uid});

                }
                else
                {
                    ViewBag.Message = "Wrong Password";
                    return View(res);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
  
            
        }

        public string DecryptAsync(string text)
        {
            var textToDecrypt = text;
            string toReturn = "";
            string publickey = "12345678";
            string secretkey = "87654321";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                toReturn = encoding.GetString(ms.ToArray());
            }
            return toReturn;
        }


    }
}

