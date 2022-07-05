using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Bll;
using Bll.KodFunction;
using WebApi.Models;

namespace WebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        // GET: api/Category
        public object Get()
        {//searchArt- כל הקטגוריות יכולות להשתתף בחיפוש
            //addCat, HandleCat- עבור בדיקה האם קיימת כבר קטגוריה
            return BllCategory.getAllCategories();
        }

        // GET: api/Category/5
        public object Get(int id)
        {   //showArt- ניתן להעביר את המאמר רק לקטגוריות שיש להן ילדים
            if (id == -1)
                return BllCategory.getAllCategories().Where(cat => cat.AmountArticals>0);
            //catAlbum- מציג את כל הקטגוריות ששייכות לאב מסוים או שאין להן אב, לפי הערך 
            if (id == 0)
                return BllCategory.getAllCategories().Where(cat => cat.ParentID == null);
            return BllCategory.getAllCategories().Where(cat=>cat.ParentID == id);
        }

        // POST: api/Category
        public int Post(AddArtsToCatFromReact value)
        {//addArtsToCat.tsx
            try { 
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string root = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
                foreach (NewArtFromReact file in value.Arts)
                {
                    if (!file.Name.Equals("") && !file.Path.Equals(""))
                    {
                        string originalFileName = String.Concat(root, "\\" + file.Path.Substring(file.Path.LastIndexOf("/") + 1));
                        while (File.Exists(originalFileName))
                        {
                            originalFileName = originalFileName.Insert(originalFileName.LastIndexOf('.'), "~");
                        }
                        using (WebClient wc = new WebClient())
                        {
                            try { wc.DownloadFile(file.Path, originalFileName);}
                            catch { return -1; }
                            
                            try
                            {
                                dic.Add(file.Name, originalFileName);
                            }
                            catch
                            {
                                int i = 1;
                                while (i!=-1)
                                {
                                    try
                                    {
                                        dic.Add(file.Name+(i), originalFileName);
                                        i = -1;
                                    }
                                    catch { 
                                        i++;
                                    }
                                }                        
                            }
                        }
                    }
                }
                HeadFunction.AddArtsToCat(value.Id, dic);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        // PUT: api/Category
        public void Put( NewCatFromReact value)
        {//addCat.tsx
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string root = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
                foreach (NewArtFromReact file in value.Arts)
                {
                    if (!file.Name.Equals("") && !file.Path.Equals(""))
                    {
                        string originalFileName = String.Concat(root, "\\" + file.Path.Substring(file.Path.LastIndexOf("/") + 1));
                        while (File.Exists(originalFileName))
                        {
                            originalFileName = originalFileName.Insert(originalFileName.LastIndexOf('.'), "~");
                            //File.Delete(originalFileName);
                        }
                        using (WebClient wc = new WebClient())
                        {
                            try { wc.DownloadFile(file.Path, originalFileName); }
                            catch { }

                            try
                            {
                                dic.Add(file.Name, originalFileName);
                            }
                            catch
                            {
                                int i = 1;
                                while (i != -1)
                                {
                                    try
                                    {
                                        dic.Add(file.Name + (i), originalFileName);
                                        i = -1;
                                    }
                                    catch
                                    {
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                }

                HeadFunction.AddNewCat(value.Kod, value.Name, dic);
            }
            catch { throw; }

        }

        // DELETE: api/Category/5
        //public void Delete(int id)
        //{
        //}
    }
}
