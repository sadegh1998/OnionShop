using _0_Framework.Domain;
using ShopManagement.ApplicationContract.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastracture.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context) 
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide {
            Id = x.Id ,
            Title = x.Title,
            BtnText = x.BtnText,
            Heading = x.Heading,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Text = x.Text,
            Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x=> new SlideViewModel {
           
                Id = x.Id ,
                Picture = x.Picture,
                Heading = x.Heading,
                Title = x.Title,    
                CreationDate = x.CreationDate.ToString(),
                IsRemove = x.IsRemove
            
            }).ToList();
        }
    }
}
