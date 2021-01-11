using AutoMapper;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    public class PurchaseListsController : CrudController<PurchaseListDto, PurchaseList>
    {
        public PurchaseListsController(ICrudService<PurchaseListDto, PurchaseList> purchaseListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(purchaseListService, mapper, loggerFactory)
        {
        }
    }
}
