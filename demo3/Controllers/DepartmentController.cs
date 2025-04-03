using demo.BLL.DataTransferObjects.Department;
using demo.BLL.Services;
using demo.DAL.Models;
using demo.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace demo3.Controllers;

public class DepartmentController(IDepartmentServices departmentServices,
    IWebHostEnvironment webHostEnvironment, ILogger<DepartmentController> logger) : Controller
{
    private readonly IDepartmentServices _departmentServices = departmentServices;
    public readonly IWebHostEnvironment _WebHostEnvironment = webHostEnvironment;
    private readonly ILogger<DepartmentController> _logger = logger;

    [HttpGet]
    public IActionResult Index()
    {
        var departments = _departmentServices.GetAll();
        ViewBag.Departments = "hello from department";
        return View(departments);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(DepartmentRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        string message = string.Empty;
        try
        {
            var result = _departmentServices.Add(request);
            if (result > 0)
                message = $"Department {request.Name} is Created";
            else
                message = $"Can't create department {request.Name}";
            TempData["Message"] = message ;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (_WebHostEnvironment.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                _logger.LogError(ex.Message);
            return View(request);
            // log for production
            throw;
        }
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var department = _departmentServices.GetById(id.Value);
        if (department is null)
            return NotFound();

        return View(department);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var department = _departmentServices.GetById(id.Value);
        if (department is null)
            return NotFound();
        return View(department.ToUpdateRequest());
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int id, DepartmentUpateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        if (id != request.Id)
            return BadRequest();
        try
        {
            var result = _departmentServices.Update(request);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't create Department now !!");
            return View(request);
        }
        catch (Exception ex)
        {
            if (_WebHostEnvironment.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                _logger.LogError(ex.Message);
            return View(request);
            throw;
        }
    }

    public IActionResult Delete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var department = _departmentServices.GetById(id.Value);
        if (department is null)
            return NotFound();
        return View(department);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult ConfirmDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var result = _departmentServices.Delete(id.Value);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            // send data to index action to return it to index view
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (_WebHostEnvironment.IsProduction())
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, "Can't delete department now!");
            }

            else
                ModelState.AddModelError(string.Empty, ex.Message);

            // send data to index action to return it to index view
            return RedirectToAction(nameof(Index));
            throw;
        }
    }
}
