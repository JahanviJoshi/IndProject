using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TicketGenerator.Models;
using TicketGenerator.Services;

namespace TicketGenerator.Controllers
{
    public class RegistrationController : Controller
    {
        private  IService<Login, int> regserv;

        public RegistrationController(IService<Login, int> regserv)
        {
            this.regserv = regserv;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var res = new Login();
            return View(res);
        }

        [HttpPost]
        public IActionResult Create(Login login)
        {
            if(ModelState.IsValid)
            {
                
                login.SignIn = DateTime.Now;
                var password = EncryptAsync(login.PassWord).Result;
                string str = login.FirstName.Substring(0, 4);
                string str2 = login.LastName.Substring(0, 2);
                
                string strnew = str + str2;
                login.UserName = strnew;
                login.PassWord =password;
                var res = regserv.CreateAsync(login).Result;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Wrong EmailID";
                return View(login);
            }
        }

        public async Task<string> EncryptAsync(string message)
        {
            var textToEncrypt = message;
            string toReturn = string.Empty;
            string publicKey = "12345678";
            string secretKey = "87654321";
            byte[] secretkeyByte = { };
            //UTF: UTF-8 is an encoding system for Unicode. It can translate any
            //Unicode character to a matching unique binary string, and can also
            //translate the binary string back to a Unicode character. This is the meaning
            //of “UTF”, or “Unicode Transformation Format.”
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publicKey);
            //MemoryStream:Creates a stream whose backing store is memory.
            MemoryStream ms = null;
            //CryptoStream:Defines a stream that links data streams to cryptographic transformations.
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            //DESCryptoServiceProvider:Defines a wrapper object to access the cryptographic service provider
            //(CSP) version of the Data Encryption Standard (DES) algorithm.
            //This class cannot be inherited.
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                toReturn = Convert.ToBase64String(ms.ToArray());
            }
            return toReturn;

        }


       
    }
}
