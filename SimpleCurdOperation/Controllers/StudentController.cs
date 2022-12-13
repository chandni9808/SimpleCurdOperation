using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCurdOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCurdOperation.Controllers
{ //this is controller file //step:6
    public class StudentController : Controller 
    {
        private readonly StudentContext _Db;//step:9
        public StudentController(StudentContext Db)
        {
            _Db = Db;
        }
        public IActionResult StudentList() //step:7 add view 
        {
            try
            {
                //var stdList = _Db.tbl_Student.ToList(); 

                var stdList = from a in _Db.tbl_Student //two table join data
                              join b in _Db.tbl_Departments
                              on a.DepID equals b.ID
                              into Dep
                              from b in Dep.DefaultIfEmpty()

                              select new Student
                              {
                                  ID=a.ID,
                                  Name=a.Name,
                                  Fname=a.Fname,
                                  Mobile=a.Mobile,
                                  Email=a.Email,
                                  Description=a.Description,
                                  DepID=a.DepID,
                                  
                                  Department=b==null?"":b.Department
                              };

                return View(stdList);
            }
            catch(Exception ex)
            {
                return View();
            }
      
        }
        
        public IActionResult Create(Student obj) //step:10 create method
        {
            loadDDL();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student obj)   //step:11 create view //edit record insert record
        {
            try //data save in create 
            {
                if(ModelState.IsValid)
                {
                    if(obj.ID==0)
                    {
                        _Db.tbl_Student.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();
                    }
                 
                    return RedirectToAction("StudentList");
                }
                return View(obj);
            }
            catch(Exception ex)
            {
                return RedirectToAction("StudentList");
            }
        } 




        public async Task<IActionResult> DeleteStd(int id)
        {
            try
            {
                var std =await _Db.tbl_Student.FindAsync(id);
                if(std!=null)
                {
                    _Db.tbl_Student.Remove(std);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("StudentList");
            }
            catch(Exception ex)
            {
                return RedirectToAction("StudentList");
            }
        }

        private void loadDDL()  //record insert drop down
        {
            try
            {
                List<Departments> depList = new List<Departments>();
                depList = _Db.tbl_Departments.ToList();
                depList.Insert(0, new Departments { ID = 0, Department = "Please Select" });
                ViewBag.DepList = depList;
            }
            catch(Exception ex)
            {

            }
        }
    }
}
