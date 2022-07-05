using Bll.KodFunction;
using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class BllArtical
    {
        public static int AddArtical(DtoArtical c)
        {
            
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                
                try
                {
                    ArticalTable item = c.DtoToDal();
                    db.ArticalTables.Add(item);
                    db.SaveChanges();
                    return item.Id;
                }
                catch
                {
                    throw;
                }
            }
            
        }

        public static void deleteArtical(int id)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                try
                {
                    db.ArticalTables.Remove(db.ArticalTables.FirstOrDefault(item => item.Id == id));
                    db.SaveChanges();
                }
                catch {//בדר"כ אם לא הצליח למחוק זה לא מאוד קריטי למשתמש
                       }

            }
        }

        public static DtoArtical searchArtById(int id)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {   
                 return DtoArtical.DalToDto(db.ArticalTables.FirstOrDefault(item => item.Id == id));
            }
        }
        public static Dictionary<int, DtoArtical> getAllArticalsById()
        {
            Dictionary<int, DtoArtical> result = new Dictionary<int, DtoArtical>();
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                foreach (var item in db.ArticalTables)
                    result.Add(item.Id, DtoArtical.DalToDto(item));
                return result;
            }
        }

        public static List< DtoArtical> searchArtsByNmae(string name)
        {
            using (shortSortDBEntities db = new shortSortDBEntities())
            {
                Dictionary<DtoArtical, int> map = new Dictionary<DtoArtical, int>();
                foreach (var item in db.ArticalTables)
                {
                    bool flag = false;
                    string[] itemWords = item.Title.Split(' ');
                    string[] nameWords = name.Split(' ');
                    for (int i = 0; i < itemWords.Length && !flag; i++)
                    {
                        string itemWord = itemWords[i];
                        for (int j = 0; j < nameWords.Length && !flag; j++)
                        {
                            string nameWord = nameWords[j];
                            int amount = LevenshteinDistance.Calculate(itemWord, nameWord);
                            if (amount < Math.Min(itemWord.Length, nameWord.Length)/2)
                            {
                                map.Add(DtoArtical.DalToDto(item), amount);
                                flag = true;
                            }
                        }

                    }
                }
                map.OrderBy(i => i.Value);
                return map.Keys.ToList();
            }

        }
    }
}
