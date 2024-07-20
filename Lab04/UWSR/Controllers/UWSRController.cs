using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UWSR.Models;

namespace UWSR.Controllers
{
    public class UWSRController : Controller
    {
        private readonly UWSRDbContext _context;

        public UWSRController(UWSRDbContext context)
        {
            _context = context;
        }

        public IActionResult Uwsref()
        {
            if (HttpContext.Session.GetString("UserRole") == null)
            {
                SetMode("guest");
            }
            var model = _context.WSREFs;
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            ViewBag.urrentSessionId = HttpContext.Session.Id;

            return View(model);
        }

        public IActionResult SetMode(string mode)
        {
            HttpContext.Session.SetString("UserRole", mode);
            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult EnterPassword(string password)
        {
          
                SetMode("owner");
            
            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult ResetPassword(string password)
        {
            SetMode("guest");

            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult AddLink(WSREF wsref)
        {
            if (HttpContext.Session.GetString("UserRole") == "owner")
            {
                _context.WSREFs.Add(wsref);
                _context.SaveChanges();
            }
            return RedirectToAction("Uwsref", _context.WSREFs.ToList());
        }

        [HttpPost]
        public IActionResult UpdateLink(WSREF wsref)
        {
            if (HttpContext.Session.GetString("UserRole") == "owner")
            {
                var existingLink = _context.WSREFs.Find(wsref.Id);
                if (existingLink != null)
                {
                    existingLink.Url = wsref.Url;
                    existingLink.Description = wsref.Description;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult DeleteLink(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "owner")
            {
                var linkToDelete = _context.WSREFs.Find(id);

                if (linkToDelete != null)
                {
                    _context.WSREFs.Remove(linkToDelete);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult IncreaseCounter(int linkId)
        {
            var link = _context.WSREFs.Find(linkId);

            if (link != null && (HttpContext.Session.GetString("UserRole") == "owner"))
            {
                link.Plus++;
                _context.SaveChanges();
                return RedirectToAction("Uwsref");
            }

            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult DecreaseCounter(int linkId)
        {
            var link = _context.WSREFs.Find(linkId);

            if (link != null && (HttpContext.Session.GetString("UserRole") == "owner"))
            {
                link.Minus--;
                _context.SaveChanges();
                return RedirectToAction("Uwsref");
            }

            return RedirectToAction("Uwsref");
        }

        [HttpGet]
        public IActionResult GetLinkValues(int linkId)
        {
            var link = _context.WSREFs.Find(linkId);

            if (link != null)
            {
                var linkValues = new
                {
                    Plus = link.Plus,
                    Minus = link.Minus
                };

                return Json(linkValues);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult AddComment(WSREFCOMMENT comment)
        {
            if (HttpContext.Session.GetString("UserRole") == "owner" || HttpContext.Session.GetString("UserRole") == "guest")
            {
                comment.Stamp = DateTime.UtcNow;
                comment.SessionId = HttpContext.Session.Id;
                _context.WSREFComments.Add(comment);
                _context.SaveChanges();
            }
            return RedirectToAction("Uwsref");
        }

        [HttpPost]
        public IActionResult UpdateComment(WSREFCOMMENT updatedComment)
        {
            string cuserrole = HttpContext.Session.GetString("UserRole");
            string cSessionId = HttpContext.Session.Id;

            if (cuserrole == "owner" || CanUserDeleteComment(updatedComment.Id, cSessionId))
            {
                var existingComment = _context.WSREFComments.Find(updatedComment.Id);

                if (existingComment != null)
                {
                        existingComment.ComText = updatedComment.ComText;
                        existingComment.Stamp = DateTime.UtcNow;
                        _context.SaveChanges();
                }
            }

            return RedirectToAction("Uwsref");
        }


        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            string cuserrole = HttpContext.Session.GetString("UserRole");
            string cSessionId = HttpContext.Session.Id;

            if (cuserrole == "owner" || CanUserDeleteComment(id, cSessionId))
            {
                var commentToDelete = _context.WSREFComments.Find(id);

                if (commentToDelete != null)
                {
                    _context.WSREFComments.Remove(commentToDelete);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Uwsref");
        }

        private bool CanUserDeleteComment(int commentId, string userSessionId)
        {
            var comment = _context.WSREFComments.Find(commentId);

            return comment != null && comment.SessionId == userSessionId;
        }

        public IActionResult ShowComments(int id)
        {
            var link = _context.WSREFs.Include(w => w.Comments).FirstOrDefault(w => w.Id == id);

            if (link != null)
            {
                ViewBag.CurrentLink = link;

                ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
                ViewBag.СurrentSessionId = HttpContext.Session.Id;

                return PartialView("_CommentsPartial", link.Comments);
            }

            return PartialView("_CommentsPartial", new List<WSREFCOMMENT>());
        }
    }
}
