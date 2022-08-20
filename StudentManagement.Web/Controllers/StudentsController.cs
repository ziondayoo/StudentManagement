using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.IServices;
using StudentManagement.Data;
using StudentManagement.Data.IRepositories;
using StudentManagement.Dtos.StudentDtos;
using StudentManagement.Models;

namespace StudentManagement.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepo;

        public StudentsController(IStudentService studentService, IStudentRepository studentRepo)
        {
            _studentService = studentService;
            _studentRepo = studentRepo;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _studentService.GetAllStudentsAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                return View(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentRequestDto student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.AddStudentAsync(student);
                }
                catch (Exception)
                {
                    View(student);
                }
 
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }



        
         // GET: Students/Edit/5
        
         public async Task<IActionResult> Edit(int id)
        {
            var data =  await _studentRepo.GetStudentByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }
     
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,FirstName,LastName,FavouriteQuote,CreatedDate,LastUpdatedDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.EditStudentAsyn(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            await _studentService.DeleteStudentAsyn(id);
                
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*   var student = await _context.Students.FindAsync(id);*/
            /*  var _studentService.Students.Remove(student);*/
            await _studentService.DeleteStudentAsyn(id);
            return RedirectToAction(nameof(Index));
        }
/*
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }*/
    }
}
