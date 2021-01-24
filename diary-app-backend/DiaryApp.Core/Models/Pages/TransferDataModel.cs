using System;
using System.Reflection;

namespace DiaryApp.Core.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PageAreaAttribute : Attribute
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

        /// <summary>
        /// For given type of page area returns is it checked in property or not. If given type is not present in properties, throws an exception.
        /// </summary>
        /// <param name="pageAreaType">Type of page area</param>
        /// <returns></returns>
        public bool GetValueForArea<T>(T area = null) where T : MonthPageArea
        {
            Type transferDataType = GetType();
            Type pageAreaType = typeof(T);
            foreach (var property in transferDataType.GetProperties())
            {
                PageAreaAttribute areaAttribute = property.GetCustomAttribute<PageAreaAttribute>();
                if (areaAttribute.AreaType == pageAreaType)
                    return (bool)property.GetValue(this);
            }
            throw new NotSupportedException($"Type {pageAreaType} of page area is not expected at transfer data model!");
        }

        public override bool Equals(object obj)
        {
            return obj is TransferDataModel model &&
                   TransferPurchasesArea == model.TransferPurchasesArea &&
                   TransferDesiresArea == model.TransferDesiresArea &&
                   TransferGoalsArea == model.TransferGoalsArea &&
                   TransferIdeasArea == model.TransferIdeasArea;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TransferPurchasesArea, TransferDesiresArea, TransferGoalsArea, TransferIdeasArea);
        }
    }
}
