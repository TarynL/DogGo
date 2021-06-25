using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalksRepository _walksRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalksRepository walksRepository, IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _walksRepo = walksRepository;
            _ownerRepo = ownerRepository;
        }

        // GET: WalkersController
        // GET: Walkers
        //public ActionResult Index()
        //{
            
        //    List<Walker> walkers = _walkerRepo.GetAllWalkers();

        //    return View(walkers);
        //}
        public ActionResult Index()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();
            //int neighborhoodId = GetCurrentUserId();
            
            ////int ownerId = GetCurrentUserId();
            //List<Walker> neighborhoodWalkers = _walkerRepo.GetWalkersInNeighborhood(neighborhoodId);
            //List<Owner> owners = _ownerRepo.GetOwnerById(ownerId);
            ////if ()
            ////{

            //}
            //else
            //{
            return View(walkers);
            
            
        }


        // GET: Walkers/Details/5
        //public ActionResult Details(int id)
        //{
        //    Walker walker = _walkerRepo.GetWalkerById(id);

        //    if (walker == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(walker);
        //}

        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetAllWalksByWalkerId(walker.Id);

            ViewBag.Total = walks.Sum(x => x.Duration);

            WalkerProfileViewModel wpvm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };
            return View(wpvm);

        }


        // GET: WalkersController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walker walker)
        {
            try
            {
                _walkerRepo.AddWalker(walker);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walker);
            }
        }

        // GET: WalkersController/Edit/5
        public IActionResult Edit(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }
            return View(walker);
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Walker walker)
        {
            try
            {
                _walkerRepo.UpdateWalker(walker);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(walker);
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            return View(walker);
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walker walker)
        {
            try
            {
                _walkerRepo.DeleteWalker(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(walker);
            }
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
