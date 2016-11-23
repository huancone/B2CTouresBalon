using System.Data.Entity;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Create([Bind(Include = "FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,CREDITCARDTYPE,CREDITCARDNUMBER")] CUSTOMER customer)
        {
            if (!ModelState.IsValid) return View(customer);
            using (var db = new ClientContext())
            {
                var c = new CUSTOMER
                {
                    ADDRESS = customer.ADDRESS,
                    FNAME = customer.FNAME,
                    LNAME = customer.LNAME,
                    EMAIL = customer.EMAIL,
                    PHONENUMBER = customer.PHONENUMBER,
                    PASSWORD = customer.PASSWORD,
                    STATUS = "PLATEADO"
                };
                db.CUSTOMER.Add(c);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
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
        public async Task<ActionResult> Edit([Bind(Include = "CUSTID,FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,CREDITCARDTYPE,CREDITCARDNUMBER")] CUSTOMER customer)
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
