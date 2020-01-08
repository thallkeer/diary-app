﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class ListService<TList, TItem> : CrudService<TList>, IListService<TList, TItem> 
        where TList : ListBase<TItem> 
        where TItem : ListItemBase<TList>
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

        public TList GetByPageID(int pageID)
        {
            return dbSet.FirstOrDefault(el => el.PageID == pageID);
        }

        public List<TList> GetListsByPageID(int pageID)
        {
            return dbSet.Where(el => el.PageID == pageID).ToList();
        }

        public async Task UpdateItem(TItem item)
        {
            await itemsService.Update(item);
        }
    }
}
