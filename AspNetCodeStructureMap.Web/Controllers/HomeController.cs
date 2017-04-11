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
        private readonly IRepository customerRepository;
        private readonly IStaffServiceManager staffSvcManager;
        public HomeController(IContainer resolver, IStaffServiceManager staffSvcManager)
        {
            this.staffRepo = resolver.GetInstance<IRepository>("StaffDataRepository");
            this.customerRepository = resolver.GetInstance<IRepository>("CustomerDataRepository");
            this.staffSvcManager = staffSvcManager;
        }

        //public HomeController()
        //{
        //}

        public IActionResult Index()
        {
            List<string> types = new List<string>();
            types.Add(this.staffRepo.GetType().Name);
            types.Add(this.customerRepository.GetType().Name);
            types.Add(this.staffSvcManager.GetType().Name);
            return this.View(types);
        }
    }
}
