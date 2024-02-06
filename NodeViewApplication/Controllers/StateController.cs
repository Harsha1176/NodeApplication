using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodeViewApplication.Data;
using NodeViewApplication.Models;

namespace NodeViewApplication.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context; 
        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //Loop and add the Parent Nodes.
            foreach (State type in _context.State)
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Title });
            }

            //Loop and add the Child Nodes.
            foreach (City subType in _context.City)
            {
                nodes.Add(new TreeViewNode { id = subType.StateId.ToString() + "-" + subType.Id.ToString(), parent = subType.StateId.ToString(), text = subType.Name });
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }




        
        
    }
}
