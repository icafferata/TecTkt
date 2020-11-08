namespace TecTkt.Backend.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Backend.Models;
    using Common.Models;
    using TecTkt.Backend.Helpers;

    public class PaisesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Paises
        public async Task<ActionResult> Index()
        {
            return View(await this.db.Pais.OrderBy(p => p.Name).ToListAsync());
        }

        // GET: Paises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await this.db.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaisView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Paises";

                if(view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                // Convertir PaisView a Pais
                var pais = this.ToPais(view, pic);

                this.db.Pais.Add(pais);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Pais ToPais(PaisView view, string pic)
        {
            return new Pais
            {
                Name = view.Name,
                Code = view.Code,
                CountryId = view.CountryId,
                ImagePath = pic,
                IntrastatCode = view.IntrastatCode,
                Remarks = view.Remarks,
                VatDigits = view.VatDigits
            };
        }

        // GET: Paises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pais = await this.db.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(pais);
            return View(view);
        }

        private PaisView ToView(Pais pais)
        {
            return new PaisView
            {
                Name = pais.Name,
                Code = pais.Code,
                CountryId = pais.CountryId,
                ImagePath = pais.ImagePath,
                IntrastatCode = pais.IntrastatCode,
                Remarks = pais.Remarks,
                VatDigits = pais.VatDigits
            };
        }

        // POST: Paises/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PaisView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Paises";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var pais = this.ToPais(view, pic);
                this.db.Entry(pais).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Paises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await this.db.Pais.FindAsync(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var pais = await this.db.Pais.FindAsync(id);
            this.db.Pais.Remove(pais);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
