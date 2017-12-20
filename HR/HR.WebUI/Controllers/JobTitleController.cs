using HR.DAL.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace HR.WebUI.Controllers
{
    public class JobTitleController : Controller
    {
        HrContext context;
        public JobTitleController()
        {
            context = new HrContext();
        }
        public ActionResult Index()
        {
            var model = context.JobTitles;
            return View(model);
        }

        public ActionResult Edit(int id=0)
        {
            JobTitle model = id==0 ? new JobTitle() : context.JobTitles.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(JobTitle j)
        {
            context.JobTitles.AddOrUpdate(j);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var ep = context.EmpPromotions.Where(x => x.JobTitleId == id);

            JobTitle t = context.JobTitles.FirstOrDefault(x => x.JobTitleId == id);
            context.EmpPromotions.RemoveRange(ep);
            context.JobTitles.Remove(t);
            context.SaveChanges();
            return RedirectToAction("Index",context.JobTitles);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}