using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoArtical
    {
        public DtoArtical()
        {
        }
        public DtoArtical(string Title, string Link)
        {
            this.Title = Title;
            this.Link = Link;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public static DtoArtical DalToDto(ArticalTable w)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<ArticalTable, DtoArtical>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<DtoArtical>(w);

        }
        public static List<DtoArtical> DalToDto(List<ArticalTable> arts)
        {
            List<DtoArtical> l = new List<DtoArtical>();
            foreach (var art in arts)
                l.Add(DalToDto(art));
            return l;
        }
        public ArticalTable DtoToDal()
        {
            var config = new MapperConfiguration(cfg =>
                     cfg.CreateMap<DtoArtical, ArticalTable>()
                 );
            var mapper = new Mapper(config);
            return mapper.Map<ArticalTable>(this);
        }
    }
}
