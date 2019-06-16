using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTaskApplication.Models;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;



namespace MyTaskApplication.Controllers
{
    public class DocumentController : Controller
    {
        // Get All Documents - ELMA FileStorage
        public ActionResult Index(string sortorder, string searchstring)
        {
            // Sorting Documents parameters
            ViewBag.Message = "ELMA FileStorage";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortorder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortorder == "Date" ? "Date_desc" : "Date";
            ViewBag.AuthorSortParm = sortorder == "Author" ? "Author_desc" : "Author";

            // Using Nhibernate to get all documents
            using (ISession session = NhibernateSession.OpenSession()) 
            {
                var documents = session.QueryOver<Documents>();

                if (!String.IsNullOrEmpty(searchstring))
                {
                    documents = documents.WhereRestrictionOn(c => c.DocumentName)
                    .IsInsensitiveLike(String.Format("%{0}%", searchstring));
                }
                // Switch Sorting : (Filename, Date, Author)
                switch (sortorder)
                {
                    case "Name_desc":
                        documents = documents.OrderBy(s => s.DocumentName).Desc;
                        break;
                    case "Name":
                        documents = documents.OrderBy(s => s.DocumentName).Asc;
                        break;
                    case "Date_desc":
                        documents = documents.OrderBy(s => s.DocumentDate).Desc;
                        break;
                    case "Date":
                        documents = documents.OrderBy(s => s.DocumentDate).Asc;
                        break;
                    case "Author_desc":
                        documents = documents.OrderBy(s => s.DocumentAuthor).Desc;
                        break;
                    case "Author":
                        documents = documents.OrderBy(s => s.DocumentAuthor).Asc;
                        break;
                    default:
                        documents = documents.OrderBy(s => s.DocumentName).Asc;
                        break;
                }
                var result = documents.List();
                return View(result);
            }
        }

        [HttpPost]
        //Open uploaded files
        public FileResult OpenFile(int? DocumentID)
        {
            byte[] bytes;
            string fileName, contentType;
            //using Hnibernate for ELMA'
            //Connect to database
            using (ISession session = NhibernateSession.OpenSession())
            {
                List<Object[]> docs = session.CreateSQLQuery("exec sp_openfile @Id=:ID")
                    .AddScalar("DocumentName", NHibernateUtil.String)
                    .AddScalar("DocumentType", NHibernateUtil.String)
                    .AddScalar("DocumentBinaryFile", NHibernateUtil.BinaryBlob)
                    .SetParameter("ID", DocumentID)
                    .List<Object[]>().ToList();

                {
                    fileName = docs[0][0].ToString();
                    contentType = docs[0][1].ToString();
                    bytes = (byte[])(docs[0][2]);
                    
                }
                return File(bytes, contentType, fileName);
            }
        }
            
                   
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]

        public ActionResult UploadFile(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                //display only postedfile.filename, not full path
                var filename = Path.GetFileName(postedFile.FileName);
                var shortname = HttpUtility.HtmlEncode(filename);
                // Using Nhibernate to upload file
                using (ISession session = NhibernateSession.OpenSession())
                {
                    IQuery query = session.CreateSQLQuery("exec sp_add_document @DocumentName=:Name,@DocumentType=:Type,@DocumentAuthor=:Author,@DocumentBinaryFile=:File");
                    query.SetParameter("Name", shortname);
                    query.SetParameter("Type", postedFile.ContentType);
                    query.SetParameter("Author", User.Identity.Name);
                    query.SetParameter("File", bytes, NHibernateUtil.BinaryBlob);
                    // it was very hard for me. Binary data have truncated to 8000KB,
                    //I tried a lot of solutions, but that only helped.
                    query.ExecuteUpdate();
                }
            }
            else
            {
                ViewBag.Message = "You need to upload file";
            }
            return RedirectToAction("Index", "Document");
        }

    }
}

