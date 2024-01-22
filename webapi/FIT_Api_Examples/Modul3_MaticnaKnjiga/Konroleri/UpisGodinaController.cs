using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Konroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpisGodinaController : ControllerBase
    {


        private readonly ApplicationDbContext _dbContext;

        public UpisGodinaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("/GetUpisiStudenta")]
        public ActionResult<List<UpisGodina>> GetAllUpisi(int studentid)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("nije logiran");


            var upisi = _dbContext.UpisGodina.Include("evidentirao").Include("student").Include("akademskaGodina").Where(x => x.studentid == studentid).ToList();



            return Ok(upisi);
        }


        [HttpPost]
        [Route("/DodajUpis")]

        public ActionResult DodajUpis([FromBody] UpisGodinaVM x) // 1 
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("nije logiran");

            if(_dbContext.UpisGodina.ToList().Exists(u=> u.studentid == x.studentid && u.godinaStudija == x.godinaStudija))
            {
                if (x.obnova)
                {
                    var noviUpis = new UpisGodina()
                    {
                        studentid = x.studentid,
                        evidentiraoid = x.evidentiraoid,
                        datumUpisa = x.datumUpisa,
                        akademskaGodinaid = x.akademskaGodinaid,
                        cijenaSkolarine = x.cijenaSkolarine,
                        obnova = x.obnova,
                        godinaStudija = x.godinaStudija,
                        
                    };
                    _dbContext.UpisGodina.Add(noviUpis);
                    _dbContext.SaveChanges();
                    return Ok();


                }
                else
                {
                    return BadRequest();
                }





            }
            else
            {
                var noviUpis = new UpisGodina()
                {
                    studentid = x.studentid,
                    evidentiraoid = x.evidentiraoid,
                    datumUpisa = x.datumUpisa,
                    akademskaGodinaid = x.akademskaGodinaid,
                    cijenaSkolarine = x.cijenaSkolarine,
                    obnova = x.obnova,
                    godinaStudija = x.godinaStudija,

                };
                _dbContext.UpisGodina.Add(noviUpis);
                _dbContext.SaveChanges();
                return Ok();
            }

           


         
        }

        [HttpPost]
        [Route("/OvjeraUpis")]

        public ActionResult OvjeraUpis([FromBody] OvjeraGodinaVM x) // 1 
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("nije logiran");


            var Upis = _dbContext.UpisGodina.Find(x.id);

            Upis.datumOvjere = x.datumOvjere;
            Upis.napomena = x.napomena;

            _dbContext.SaveChanges();




            return Ok();
        }

    }
}
