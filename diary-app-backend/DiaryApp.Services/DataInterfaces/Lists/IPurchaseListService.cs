using System.Threading.Tasks;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.Services.DataInterfaces.Lists
{
    public interface IPurchaseListService
    {
        Task<int> CreateAsync(TodoListDto todoListDto, int purchasesAreaId);
        Task<int> GetTodoListId(int purchaseListId);
    }
}
