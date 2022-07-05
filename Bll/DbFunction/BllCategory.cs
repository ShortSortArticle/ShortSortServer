using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class BllCategory
    {
        public static List<DtoCategory> getAllCategories()
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                List<DtoCategory> list = new List<DtoCategory>();
                list.AddRange(DtoCategory.DalToDto(db.CategoryTables.Select(item => item).ToList()));
                return list;
            }
        }
        public static int AddCategory(DtoCategory c)
        {
            CategoryTable item = c.DtoToDal();
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                db.CategoryTables.Add(item);
                db.SaveChanges();
            }
            return item.Id;
        }

        public static void AddAmountArtToCat(int id)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                CategoryTable item = db.CategoryTables.FirstOrDefault(c => c.Id == id);
                item.AmountArticals += 1;
                db.SaveChanges();
            }
        }
        public static void lesAmountArtToCat(int id)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                try
                {
                    CategoryTable item = db.CategoryTables.FirstOrDefault(c => c.Id == id);
                    item.AmountArticals -= 1;
                    db.SaveChanges();
                }
                catch { }

            }
        }

        public static List<int> getAllNumArt()
        {// מחזיר רשימה של כמות המאמרים עבור כל הקטגוריות לפי הרשום הטבלה
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                return (db.CategoryTables.Where(item => item.AmountArticals > 0).Select(item => item.AmountArticals)).ToList();
            }
        }

        public static List<int> getAllNotParentsId()
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                return (db.CategoryTables.Where(item => item.AmountArticals > 0).Select(item => item.Id)).ToList();
            }
        }

        public static void updateCat(DtoCategory newCatDetails)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                DtoCategory item = DtoCategory.DalToDto( db.CategoryTables.FirstOrDefault(x => x.Id == newCatDetails.Id));
                item.ParentID = newCatDetails.ParentID;
                item.Kod = newCatDetails.Kod;
                item.Desciption = newCatDetails.Desciption;
                db.SaveChanges();
            }
        }
        public static Dictionary<int, DtoCategory> getAllCatsById()
        {
            Dictionary<int,DtoCategory> dic = new Dictionary<int,DtoCategory>();
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                foreach (var item in db.CategoryTables)
                    dic.Add(item.Id, DtoCategory.DalToDto(item));
                return dic;
            }
        }
    }
}
