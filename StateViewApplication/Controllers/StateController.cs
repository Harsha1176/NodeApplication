using Microsoft.AspNetCore.Mvc;
using StateViewApplication.Data;
using StateViewApplication.Models;
using Newtonsoft.Json;

namespace StateViewApplication.Controllers
{
    public class StateController : Controller
    {
        private readonly DatabaseHelper _databaseHelper;

        public StateController(string connectionString)
        {
            _databaseHelper = new DatabaseHelper(connectionString);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = _databaseHelper.GetNodes();

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }



        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            if (string.IsNullOrEmpty(selectedItems))
            {
                // Handle the error condition, e.g. redirect back to the index page
                return RedirectToAction("Index");
            }

            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);

            foreach (TreeViewNode item in items)
            {
                if (item.id.Contains('-'))
                {
                    _databaseHelper.InsertNode(item);
                }
                else
                {
                    _databaseHelper.UpdateNode(item);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetNodes()
        {
            List<TreeViewNode> nodes = _databaseHelper.GetNodes();
            return Json(nodes);
        }
    }
}
