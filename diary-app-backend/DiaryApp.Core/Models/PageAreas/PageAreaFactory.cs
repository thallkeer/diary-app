using System;

namespace DiaryApp.Core.Models.PageAreas
{
    public interface IPageAreaFactory
    {
        /// <summary>
        /// Creates new page area (with default initialization) for page
        /// </summary>
        /// <param name="page">Page that owns page area</param>
        /// <returns></returns>
        PageAreaBase CreatePageArea(PageBase page);
    }

    public class PageAreaFactoryCreator
    {
        public IPageAreaFactory GetFactoryByAreaType(PageAreaType areaType) =>
            areaType switch
            {
                PageAreaType.Purchases => new PurchasesAreaFactory(),
                PageAreaType.Goals => new GoalsAreaFactory(),
                PageAreaType.Desires => new DesiresAreaFactory(),
                PageAreaType.Ideas => new IdeasAreaFactory(),
                _ => throw new NotImplementedException($"No such factory for page area {areaType}!")
            };
    }

    public class GoalsAreaFactory : IPageAreaFactory
    {
        public PageAreaBase CreatePageArea(PageBase page) => new GoalsArea(page, true);
    }

    public class DesiresAreaFactory : IPageAreaFactory
    {
        public PageAreaBase CreatePageArea(PageBase page) => new DesiresArea(page, true);
    }

    public class IdeasAreaFactory : IPageAreaFactory
    {
        public PageAreaBase CreatePageArea(PageBase page) => new IdeasArea(page, true);
    }

    public class PurchasesAreaFactory : IPageAreaFactory
    {
        public PageAreaBase CreatePageArea(PageBase page) => new PurchasesArea(page, true);
    }
}
