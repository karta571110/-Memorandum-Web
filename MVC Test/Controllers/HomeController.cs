using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_Test.Models;

namespace MVC_Test.Controllers
{
    public class HomeController : Controller
    {

        dbToDoEntities1 db = new dbToDoEntities1();//建立dbToDoEntities類別的db物件
        // GET: Home
        public ActionResult Index()
        {

            var todos = db.tToDo.OrderByDescending(m => m.fDate).ToList();//將tToDo資料表內的資料依fDate欄位進行遞減排序並轉成串列再將結果指定todos變數。
            return View(todos);//將todos待辦事項的所有紀錄傳到Index.cshtml的View檢視頁面。
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public　ActionResult Create(string fTitle,string fImage,DateTime fDate)
        {
            tToDo todo = new tToDo();
            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            db.tToDo.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var todo = db.tToDo.Where(m => m.fid == id).FirstOrDefault();

            db.tToDo.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var todo = db.tToDo.Where(m => m.fid == id).FirstOrDefault();
            return View(todo);
        }
        [HttpPost]
        public ActionResult Edit(int fId,string fTitle,string fImage,DateTime fDate)
        {
            var todo = db.tToDo.Where(m => m.fid == fId).FirstOrDefault();

            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}