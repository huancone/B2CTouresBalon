using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace B2CTouresBalon.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // POST: Customer/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            using (var db = new ClientContext())
            {
                var customer = await db.CUSTOMER.FindAsync(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CUSTID,FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,CREDITCARDTYPE,CREDITCARDNUMBER,STATUS")] CUSTOMER customer)
        {
            if (!ModelState.IsValid) return View(customer);
            using (var db = new ClientContext()) { 
                db.CUSTOMER.Add(customer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home");}
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            using (var db = new ClientContext())
            {
                var customer = await db.CUSTOMER.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
            }
                
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,CREDITCARDTYPE,CREDITCARDNUMBER")] CUSTOMER customer)
        {
            if (!ModelState.IsValid) return View(customer);
            using (var db = new ClientContext())
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
