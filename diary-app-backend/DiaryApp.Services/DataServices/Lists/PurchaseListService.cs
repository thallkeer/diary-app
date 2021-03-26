using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Services.DataInterfaces.Lists;
using DiaryApp.Services.DTO.Lists;
using DiaryApp.Services.Exceptions;

namespace DiaryApp.Services.DataServices.Lists
{
    public class PurchaseListService : IPurchaseListService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public PurchaseListService(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CreateAsync(TodoListDto todoListDto, int purchasesAreaId)
        {
            Guard.Against.Null(todoListDto, nameof(todoListDto));
            var todoList = _mapper.Map<TodoList>(todoListDto);
            var entity = new PurchaseList
            {
                AreaOwnerId = purchasesAreaId,
                List = todoList
            };
            await _context.PurchaseLists.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> GetTodoListId(int purchaseListId)
        {
            var purchaseList = await _context.PurchaseLists.FindAsync(purchaseListId);
            if (purchaseList == null)
                throw new EntityNotFoundException<PurchaseList>();
            return purchaseList.ListID;
        }
    }
}