using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers.Lists
{
    public class PurchaseListsController : CrudController<PurchaseListDto, PurchaseList>
    {
        public PurchaseListsController(ICrudService<PurchaseListDto, PurchaseList> purchaseListService, IMapper mapper)
            : base(purchaseListService, mapper)
        {
        }
    }
}
