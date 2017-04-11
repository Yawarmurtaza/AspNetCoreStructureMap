using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AspNetCodeStructureMap.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using StructureMap;

namespace AspNetCodeStructureMap.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository staffRepo;
        public HomeController(IContainer injectedContainer)
        {
            this.staffRepo = injectedContainer.GetInstance<IRepository>("StaffDataRepository");
        }

        //public HomeController()
        //{
        //}

        public IActionResult Index()
        {
            List<string> types = new List<string>();
            types.Add(this.staffRepo.GetType().Name);
            return this.View(types);
        }
    }
}
