using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;

namespace ASPCMVC07.Controllers
{
    public class CalcController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.press = HttpContext.Request.Query["press"];
            ViewBag.x = 0;
            ViewBag.y = 0;
            ViewBag.z = 0;
            Boolean isCorrect = true;
            string press = (string)ViewBag.press;
            try
            {
                if (!float.TryParse(ViewBag.x, out float parsedX))
                {
                    isCorrect = false;
                }
                if (!float.TryParse(ViewBag.y, out float parsedY))
                {
                    isCorrect = false;
                }

            }
            catch (Exception)
            {
			}
       

            if (press != null)
            {
                switch (press)
                {
                    case "+": isCorrect = true; break;
                    case "-": isCorrect = true; break;
                    case "*": isCorrect = true; break;
                    case "/": isCorrect = true; break;
                    default: isCorrect = false; break;
                }
                if(!isCorrect) {
                    ViewBag.press = null;
                    ViewBag.x = null;
                    ViewBag.y = null;
                    ViewBag.z = null;
                    ViewBag.error = "ERROR: Specify correct action or parameters";
                }
            }


            return View();
        }
        [HttpPost]
        public IActionResult Sum(string x, string y, string press)
        {
            try
            {
                Boolean isCorrect = true;

                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.press = press;
                ViewBag.z = 0;
                if (!"+".Equals(press))
                {
                    ViewBag.error = "\nERROR: Specify correct action";

                    return View("Index");
                };

                if (float.TryParse(x, out float parsedX))
                {
                    ViewBag.x = parsedX;
                }
                else {
                    ViewBag.error += "\nERROR: Incorrect the first parameter";

                    isCorrect = false;
                }
                if (float.TryParse(y, out float parsedY))
                {
                    ViewBag.y = parsedY;
                }
                else
                {
                    isCorrect = false;
                    ViewBag.error += "\nERROR: Incorrect the second parameter";
                }

                if (isCorrect)
                {    ViewBag.z = parsedX + parsedY;
                
                    if (float.IsNaN((float)ViewBag.z))
                    {
                        ViewBag.error += "  " + "ERROR: The value is NaN";
                        ViewBag.z = 0;
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }
                    if (float.IsInfinity((float)ViewBag.z))
                    {
                        ViewBag.z = 0;
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                        ViewBag.error += "  " + "ERROR: The value is infinity";
                    }
                }
            }
            catch
            {
                ViewBag.error += "\nERROR: Unexpected exception";
                return View("Index");
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult Sub(string x, string y, string press)
        {
            try
            {
                Boolean isCorrect = true;

                if (!"-".Equals(press))
                {
                    ViewBag.error = "\nERROR: Specify correct action";
                    return View("Index");

                };

                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.press = press;
                ViewBag.z = 0;
                if (float.TryParse(x, out float parsedX))
                {
                    ViewBag.x = parsedX;
                }
                else
                {
                    ViewBag.error += "\nERROR: Incorrect the first parameter";

                    isCorrect = false;

                }
                if (float.TryParse(y, out float parsedY))
                {
                    ViewBag.y = parsedY;
                }
                else
                {
                    isCorrect = false;
                    ViewBag.error += "\nERROR: Incorrect the second parameter";

                }
                if (isCorrect)
                {
                    ViewBag.z = parsedX - parsedY;
                    if( float.IsInfinity((float )ViewBag.z))
                    {
                        ViewBag.z = 0;
                        ViewBag.error += "  " + "ERROR: The value is infinity";
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }
                    if (float.IsNaN((float)ViewBag.z))
                    {
                        ViewBag.error += "  " + "ERROR: The value is NaN";
                        ViewBag.z = 0;
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }
                }
            }
            catch
            {
                ViewBag.error = "\nERROR: Unexpected exception";
                return View("Index");
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult Mul(string x, string y, string press)
        {
            try
            {
                Boolean isCorrect = true;

                if (!"*".Equals(press))
                {
                    ViewBag.error += "\nERROR: Specify correct action";
                    return View("Index");

                };
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.press = press;
                ViewBag.z = 0;
                if (float.TryParse(x, out float parsedX))
                {
                    ViewBag.x = parsedX;
                }
                else
                {
                    ViewBag.error += "\nERROR: Incorrect the first parameter";

                    isCorrect = false;

                }
                if (float.TryParse(y, out float parsedY))
                {
                    ViewBag.y = parsedY;
                }
                else
                {
                    isCorrect = false;
                    ViewBag.error += "\nERROR: Incorrect the second parameter";

                }
                if (isCorrect)
                {
                    ViewBag.z = parsedX * parsedY;
                    if (float.IsNaN((float)ViewBag.z))
                    {
                        ViewBag.error += "  " + "ERROR: The value is NaN";
                        ViewBag.z = 0;
                        ViewBag.x = 0;
                        ViewBag.y = 0;

                    }
                    if (float.IsInfinity((float)ViewBag.z))
                    {
                        ViewBag.z = 0;
                        ViewBag.error += "  " + "ERROR: The value is infinity";
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }

                }
            }
            catch
            {
                ViewBag.error = "\nERROR: Unexpected exception";
                return View("Index");
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult Div(string x, string y, string press)
        {
            try
            {
                Boolean isCorrect = true;


                if (!"/".Equals(press))
                {
                    ViewBag.error += "\nERROR: Specify correct action";
                    return View("Index");

                };
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.press = press;
                ViewBag.z = 0;
                if (float.TryParse(x, out float parsedX))
                {
                    ViewBag.x = parsedX;
                }
                else
                {
                    ViewBag.error += "\nERROR: Incorrect the first parameter";

                    isCorrect = false;

                }
                if (float.TryParse(y, out float parsedY))
                {
                    ViewBag.y = parsedY;
                    if (parsedY == 0)
                        ViewBag.error += "\nERROR: Incorrect the second parameter";
                }
                else
                {
                    isCorrect = false;
                    ViewBag.error += "\nERROR: Incorrect the second parameter";

                }
                if (isCorrect)
                {
                    ViewBag.z = parsedX / parsedY;
                    if (float.IsNaN((float)ViewBag.z)) {
                        ViewBag.error += "  " + "the value is NaN";
                        ViewBag.z = 0;

                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }
                    if (float.IsInfinity((float)ViewBag.z))
                    {
                        ViewBag.error += "  " + "the value is infinity";
                        ViewBag.z = 0;
                        ViewBag.x = 0;
                        ViewBag.y = 0;
                    }
                }
            }
            catch
            {
                ViewBag.error += "ERROR: Unexpected exception";
                return View("Index");
            }
            return View("Index");
        }
    }
}
