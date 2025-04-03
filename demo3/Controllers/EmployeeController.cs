using demo.BLL.DataTransferObjects.Department;
using demo.BLL.DataTransferObjects.Employee;
using demo.BLL.Services;
using demo3.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace demo.presentaion.Controllers;

public class EmployeeController(IEmployeeServices EmployeeServices,
    IWebHostEnvironment webHostEnvironment, ILogger<EmployeeController> logger) : Controller
{
    private readonly IEmployeeServices _EmployeeServices = EmployeeServices;
    public readonly IWebHostEnvironment _WebHostEnvironment = webHostEnvironment;
    private readonly ILogger<EmployeeController> _logger = logger;

    [HttpGet]
    public IActionResult Index(string? SearchValue)
    {
        var Employees = _EmployeeServices.GetAll(SearchValue);
        return View(Employees);
    }

    [HttpGet]
    public IActionResult Create([FromServices] IDepartmentServices departmentServices)
    {
        var departments = departmentServices.GetAll();
        var items = new SelectList(departments , nameof(DepartmentResponce.Id),nameof(DepartmentResponce.Name));
        ViewBag.Departments = items;
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        try
        {
            var result = _EmployeeServices.Add(request);
            if (result > 0) 
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't create Employee now !!");
            return View(request);
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

        var Employee = _EmployeeServices.GetById(id.Value);
        if (Employee is null)
            return NotFound();

        return View(Employee);
    }
    [HttpGet]
    public IActionResult Edit(int? id, [FromServices] IDepartmentServices departmentServices)
    {
        if (!id.HasValue)
            return BadRequest();

        var Employee = _EmployeeServices.GetById(id.Value);
        if (Employee is null)
            return NotFound();
        var employeeRequest = new EmployeeUpdateRequest
        {
            Address = Employee.Address,
            Age = Employee.Age,
            Email = Employee.Email,
            HiringDate = Employee.HiringDate,
            id = id.Value,
            isActive = Employee.isActive,
            employeeType = Employee.employeeType,
            gender = Employee.gender,
            Name = Employee.Name,
            PhoneNumber = Employee.PhoneNumber,
            Salary = Employee.Salary
        };
        var departments = departmentServices.GetAll();
        var items = new SelectList(departments, nameof(DepartmentResponce.Id), nameof(DepartmentResponce.Name));
        ViewBag.Departments = items;
        return View(employeeRequest);
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int id, EmployeeUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        if (id != request.id)
            return BadRequest();
        try
        {
            var result = _EmployeeServices.Update(request);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't create Employee now !!");
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

        var Employee = _EmployeeServices.GetById(id.Value);
        if (Employee is null)
            return NotFound();
        return View(Employee);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult ConfirmDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var result = _EmployeeServices.Delete(id.Value);
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
                ModelState.AddModelError(string.Empty, "Can't delete Employee now!");
            }

            else
                ModelState.AddModelError(string.Empty, ex.Message);

            // send data to index action to return it to index view
            return RedirectToAction(nameof(Index));
            throw;
        }
    }
}
