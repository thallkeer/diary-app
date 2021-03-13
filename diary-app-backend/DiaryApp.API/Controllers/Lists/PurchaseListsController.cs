using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers.Lists
{
    public class PurchaseListsController : CrudController<PurchasesListDto, PurchaseList>
    {
        public PurchaseListsController(ICrudService<PurchasesListDto, PurchaseList> purchaseListService)
            : base(purchaseListService)
        {
        }
    }
}
