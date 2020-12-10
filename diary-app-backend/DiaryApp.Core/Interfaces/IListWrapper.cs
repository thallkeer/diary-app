using DiaryApp.Core.Models;
using System.Collections.Generic;

namespace DiaryApp.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IListWrapper<T, U>
        where T : ListBase<U>, new()
        where U : ListItemBase
    {
        T List { get; set; }
        List<U> Items { get; set; }
    }
}
