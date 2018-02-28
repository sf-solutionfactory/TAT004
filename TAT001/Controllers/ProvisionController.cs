﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;

namespace TAT001.Controllers
{
    public class ProvisionController : Controller
    {
        // GET: Provision
        public ActionResult Index()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    return RedirectToAction("Pais", "Home");
                }
            }
            return View();
        }
    }
}