﻿using DTSMCC_WebApp.Context;
using DTSMCC_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DTSMCC_WebApp.Controllers
{
    public class EmployeesController : Controller
    {
        MyContext myContext;

        public EmployeesController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // Read
        public IActionResult Index()
        {
            var getData = myContext.Employees.Include(x => x.Jobs).ToList();
            return View(getData);
        }

        // Read by Id
        public IActionResult IndexId(int id)
        {
            var getDataId = myContext.Employees.Find(id);
            return View(getDataId);
        }

        // Create
        // - Get

        // - Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees employees)
        {
            if (ModelState.IsValid)
            {
                myContext.Employees.Add(employees);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employees);
        }

        // Update
        // - Get
        public IActionResult Update()
        {
            return View();
        }
        
        // - Put
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employees employees)
        {
            return View();
        }
        
        // Delete
        // - Get
        public IActionResult Delete()
        {
            return View();
        }
        
        // - Post
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employees employees)
        {
            return View();
        }
    }
}
