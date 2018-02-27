using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = Session["UserID"].ToString();
                if (pais != null)
                    Session["pais"] = pais;
                ViewBag.pais = pais + ".svg";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                try
                {
                    string p = Session["pais"].ToString();
                }
                catch
                {
                    return RedirectToAction("Pais", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelPais(string pais)
        {
            Session["pais"] = pais;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO objUser, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (objUser.PASS != null)
                {
                    using (TAT001Entities db = new TAT001Entities())
                    {
                        Cryptography c = new Cryptography();
                        string pass = c.Encrypt(objUser.PASS);

                        var obj = db.USUARIOs.Where(a => a.ID.Equals(objUser.ID) && a.PASS.Equals(pass)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.ID.ToString();
                            Session["UserName"] = obj.NOMBRE.ToString();
                            FormsAuthentication.SetAuthCookie(obj.ID.ToString(), false);
                            if (ReturnUrl == null)
                                return RedirectToAction("Index", "Home");
                            if (ReturnUrl.Equals("/"))
                                return RedirectToAction("Index", "Home");
                            string[] ret = ReturnUrl.Split('/');
                            FormsAuthentication.RedirectFromLoginPage(obj.ID.ToString(), true);
                            return RedirectToAction(ret[ret.Length - 1], ret[ret.Length - 2]);
                        }
                        else
                            ViewBag.Message = "Usuario o contraseña incorrecta";
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult Pais()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = Session["UserID"].ToString();
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
            }
            return View();
        }
    }
}