using DiaryApp.Core.Interfaces;
using System;

namespace DiaryApp.Core.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    class PageAreaAttribute : Attribute
    {
        public PageAreaType AreaType { get; }
        public PageAreaAttribute(PageAreaType pageAreaType)
        {
            AreaType = pageAreaType;
        }
    }

    public class TransferDataModel
    {
        [PageArea(PageAreaType.Purchases)]
        public bool TransferPurchasesArea { get; set; }
        [PageArea(PageAreaType.Desires)]
        public bool TransferDesiresArea { get; set; }
        [PageArea(PageAreaType.Goals)]
        public bool TransferGoalsArea { get; set; }
        [PageArea(PageAreaType.Ideas)]
        public bool TransferIdeasArea { get; set; }

        public bool GetValueForArea(PageAreaType pageAreaType)
        {
            Type transferDataType = typeof(TransferDataModel);
            foreach (var property in transferDataType.GetProperties())
            {
                PageAreaAttribute areaAttribute = (PageAreaAttribute) Attribute.GetCustomAttribute(property, typeof(PageAreaAttribute));
                if (areaAttribute.AreaType == pageAreaType)
                    return (bool) property.GetValue(this);
            }
            return false;
        }
    }
}
