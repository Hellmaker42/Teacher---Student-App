using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcAdmin.ViewModels
{
  public class SelectCategoryViewModel
    {

        public int CatId { get; set; }
        public List<SelectListItem>? ListOfCat { get; set; }
    }
}