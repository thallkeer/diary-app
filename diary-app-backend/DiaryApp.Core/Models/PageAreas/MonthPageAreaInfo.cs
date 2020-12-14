﻿using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.Pages;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public interface IMonthPageArea<T> : IPageArea<T, MonthPage>     
        where T : PageAreaBase<MonthPage>
    {

    }

    public interface IMainPageArea<T> : IPageArea<T, MainPage>
        where T : PageAreaBase<MainPage>
    {

    }

    public interface IWeekPageArea<T> : IPageArea<T, WeekPage>
        where T : PageAreaBase<WeekPage>
    {

    }

    public abstract class PageAreaBase<TPage> : BaseEntity, IPageArea
        where TPage : PageBase
    {
        [Required]
        [MaxLength(100)]
        public string Header { get; set; } = string.Empty;
        [Required]
        public int PageId { get; set; }
        public virtual TPage Page { get; set; }

        public PageAreaBase()
        {

        }
        public PageAreaBase(TPage page, string header, bool withInitialization = false)
        {
            Page = page;
            Header = header;

            if (withInitialization)
                Initialize();
        }

        public abstract void Initialize();
    }
}
