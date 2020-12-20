using DiaryApp.Core.Models.PageAreas;
using System;

namespace DiaryApp.Core.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    class PageAreaAttribute : Attribute
    {
        public Type AreaType { get; }
        public PageAreaAttribute(Type pageAreaType)
        {
            AreaType = pageAreaType;
        }
    }

    public class TransferDataModel
    {
        [PageArea(typeof(PurchasesArea))]
        public bool TransferPurchasesArea { get; set; }

        [PageArea(typeof(DesiresArea))]
        public bool TransferDesiresArea { get; set; }

        [PageArea(typeof(GoalsArea))]
        public bool TransferGoalsArea { get; set; }

        [PageArea(typeof(IdeasArea))]
        public bool TransferIdeasArea { get; set; }

        public static TransferDataModel CreateFullTransferModel()
        {
            return new TransferDataModel
            {
                TransferDesiresArea = true,
                TransferGoalsArea = true,
                TransferIdeasArea = true,
                TransferPurchasesArea = true
            };
        }

        public bool GetValueForArea(Type pageAreaType)
        {
            Type transferDataType = typeof(TransferDataModel);
            var pageAreaAttributeType = typeof(PageAreaAttribute);
            foreach (var property in transferDataType.GetProperties())
            {
                PageAreaAttribute areaAttribute = (PageAreaAttribute) Attribute.GetCustomAttribute(property, pageAreaAttributeType);
                if (areaAttribute.AreaType == pageAreaType)
                    return (bool) property.GetValue(this);
            }
            throw new NotSupportedException($"Type {pageAreaType} of page area is not expected at transfer data model!");
        }
    }
}
