using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nasademo.Models;

namespace nasademo.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult nasademo( )
        {
            
           
            return View();
        }

        private static Dictionary<string, bool?> nasasync = new Dictionary<string, bool?>();
        [HttpPost]
        public JsonResult setstatus(string id, bool status)
        {
            lock(nasasync)
            {
                //status = status;
                if(nasasync.ContainsKey(id))
                {
                    nasasync[id] = status;
                    
                }
                else
                {
                    nasasync.Add(id, status);
                }
            }
            return new JsonResult(true);

        }

        [HttpPost]
        public JsonResult retrievestatuses()
        {
            nasastatuslist result = new nasastatuslist();
           lock(nasasync)
            {
                List<nasastatus> temp = new List<nasastatus>();
                foreach(var x in nasasync.Keys)
                {
                    temp.Add(new nasastatus
                    {
                        id = x,
                        status = nasasync[x]
                    });
                    
                }
                result.statuses = temp.ToArray();
            }
            return new JsonResult(result);

        }

        [HttpPost]
        public bool servo()
        {
            
                bool nasa82 = false;
                if(nasasync.ContainsKey("82"))
                {
                    nasa82 = nasasync["82"] ?? false;
                }
                else
                {
                    nasa82 = false;
                }

                bool nasa72 = false;
                
                if(nasasync.ContainsKey("72"))
                {
                    nasa72 = nasasync["72"] ?? false;
                }
                else
                {
                    nasa72 = false;
                }

                return nasa82 || nasa72;

        }
    }
}
