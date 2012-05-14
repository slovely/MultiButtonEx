using System.Web.Mvc;
using MultiButtonEx.Web.Models.Home;

namespace MultiButtonEx.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new DummyModel();
            model.Items.Add(new DummyItem {Id = 1, Other = "One"});
            model.Items.Add(new DummyItem {Id = 2, Other = "Two"});
            model.Items.Add(new DummyItem {Id = 3, Other = "Three"});
            return View(model);
        }

        [HttpPost]
        [MultiButtonEx("PerformAction")]
        public ActionResult PerformAction(int id, string other)
        {
            return Content(string.Format("You called PerformAction with: id={0} and other={1}", id, other));
        }

        [HttpPost]
        [MultiButtonEx("PerformAnActionWithType")]
        public ActionResult PerformAction2(DummyItem item)
        {
            return Content(string.Format("You called PerformAction2 with: id={0} and other={1}", item.Id, item.Other));
        }

        [HttpPost]
        [MultiButtonEx("PerformActionRenderedWithHelper")]
        public ActionResult PerformAction3(DummyItem item)
        {
            return Content(
                string.Format("You called PerformAction3, with a submit buttoned rendered with an HtmlHelper, data: id={0} and other={1}", item.Id, item.Other));
        }

    }
}