using System.Linq;
using System.Threading.Tasks;
using DiaryApp.Core;
using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Data.Services
{
    public abstract class ListService<TList, TItem> : CrudService<TList>, IListService<TList, TItem> 
        where TList : ListBase<TItem> 
        where TItem : ListItemBase
    {
        private readonly CrudService<TItem> itemsService;
        public ListService(ApplicationContext context) : base(context)
        {
            itemsService = new CrudService<TItem>(context);
        }

        public async Task AddItem(TItem item, int ownerId)
        {
            var eventList = await GetById(ownerId);
            if (eventList != null)
            {
                eventList.Items.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteItem(int itemID)
        {
            await itemsService.Delete(itemID);
        }

        public async Task<TItem> GetItemByID(int itemID)
        {
            return await itemsService.GetById(itemID);
        }

        public IQueryable<TItem> GetItems()
        {
            return itemsService.GetAll();
        }

        public async Task UpdateItem(TItem item)
        {
            await itemsService.Update(item);
        }
    }
}
