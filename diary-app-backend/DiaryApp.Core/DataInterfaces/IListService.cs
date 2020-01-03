using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IListService<TList, TItem> : ICrudService<TList> where TList : class
    {
        Task AddItem(TItem eventItem, int ownerId);
        Task UpdateItem(TItem eventItem);
        Task DeleteItem(int itemID);
    }
}
