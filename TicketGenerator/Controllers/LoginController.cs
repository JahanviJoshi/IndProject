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

                //var LoginCheck = ctx.Logins.ToList(); //_loginService.Get().Result;
                    var dbContest = new TickectCreatingDbContext();
                    var validation = dbContest.Logins.Where(cd => cd.EmailId == user.EmailId).FirstOrDefault();
                    

                    if (validation == null)
                    {
                        ViewBag.Message = "Wrong Email or Password";
                        return View(validation);
                    }
                    var passdecrypt = DecryptAsync(validation.PassWord);
                    if (user.PassWord == passdecrypt)
                    {
                        HttpContext.Session.SetInt32("UserId", validation.Uid);
                    //HttpContext.Session.SetInt32("RoleId", res.RoleId);
                    //HttpContext.Session.SetString("EmailId", res.EmailId);
                    
                    var LoggedInData = _loginService.Get().Result.Where(x => x.Uid == HttpContext.Session.GetInt32("UserId")).FirstOrDefault(); //HttpContext.Session.GetInt32("UserId")
                    LoggedInData.SignIn = DateTime.Now;
                    var info = ctx.Logins.Find(LoggedInData.Uid);
                    
                    ctx.Entry(info).CurrentValues.SetValues(LoggedInData);
                    await ctx.SaveChangesAsync();
                    return RedirectToAction("Create", "TicketGenrating", new { UserId = validation.Uid });

                    }
                    else
                    {
                        ViewBag.Message = "Wrong Password";
                        return View(validation);
                    }

            }
            catch (Exception ex)
                {

                 throw ex;
                }
  
            
        }

        public IActionResult LogOut()
        {
            var LoggedInData = _loginService.Get().Result.Where(x => x.Uid == HttpContext.Session.GetInt32("UserId")).FirstOrDefault(); //HttpContext.Session.GetInt32("UserId")
            LoggedInData.SignOut = DateTime.Now;
            var info = ctx.Logins.Find(LoggedInData.Uid);
            ctx.Entry(info).CurrentValues.SetValues(LoggedInData);
            ctx.SaveChanges();
            return RedirectToAction("GetUser");
        }

        public string DecryptAsync(string text)
        {
            var textToDecrypt = text;
            string toReturn = "";
            string publickey = "12345678";
            string secretkey = "87654321";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey); //will convert into ascii values
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null; //when there is deal with the byte there will be memorystream
            CryptoStream cs = null; //CryptoStream is designed to perform transformation from a stream
                                    //to another stream only and allows transformations chaining.For
                                    //instance you can encrypt a data stream then Base 64 encode the
                                    //encryption output.

            byte[] inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            //Converts the specified string, which encodes binary data as base-64 digits, to an
            //equivalent 8-bit unsigned integer array.


            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) //Defines a wrapper object to access the cryptographic service provider (CSP)
                                                                                  //of the Data Encryption Standard (DES) algorithm. This class cannot be inherited.
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length); //memory stream me write
                cs.FlushFinalBlock();//update and flush the stream for next operation
                Encoding encoding = Encoding.UTF8; //convert from ascii to normal
                toReturn = encoding.GetString(ms.ToArray());
            }
            return toReturn;
        }


    }
}

