using AutoMapper;
using LeaveManagement.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Mappings;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    public class LeaveTypesController : Controller
    {

        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;
        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        public ActionResult Index()
        {
           var leaveTypes = _repo.FindAll();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes.ToList());
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if(!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM collection)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(collection);
                }
                var leaveType = _mapper.Map<LeaveType>(collection);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Create(leaveType);
                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Somethisng went wrong......");
                    return View(collection);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateLeaveType([FromBody]LeaveTypeVM leaveTypeVM)
        {
            try
            {
                var leave = _mapper.Map<LeaveTypeVM, LeaveType>(leaveTypeVM);
                this._repo.Create(leave);
                return Ok(_repo.FindAll());
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if(!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveTypes = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveTypes);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid) {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                var isUpdated = _repo.Update(leaveType);
                if(!isUpdated)
                {
                    ModelState.AddModelError("", "Something went wrong....");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong....");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            if(!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(LeaveTypeVM model)
        {
            try
            {
                var leaveType = _mapper.Map<LeaveType>(model);
                var isDeleted =_repo.Delete(leaveType);
                if(!isDeleted)
                {
                    ModelState.AddModelError("", "unable to delete");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
