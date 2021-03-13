using System;
using System.Reflection;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Entities.Users.Settings
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class PageAreaAttribute : Attribute
    {
        public Type AreaType { get; }
        public PageAreaAttribute(Type pageAreaType)
        {
            AreaType = pageAreaType;
        }
    }
    
    /// <summary>
    /// Represents settings for automatically transferring page areas data when next month page is created.
    /// </summary>
    public class PageAreaTransferSettings : AppSettings
    {
        [PageArea(typeof(PurchasesArea))]
        public bool TransferPurchasesArea { get; set; }

        [PageArea(typeof(DesiresArea))]
        public bool TransferDesiresArea { get; set; }

        [PageArea(typeof(GoalsArea))]
        public bool TransferGoalsArea { get; set; }

        [PageArea(typeof(IdeasArea))]
        public bool TransferIdeasArea { get; set; }
        
        /// <summary>
        /// For given type of page area returns is it checked in property or not. If given type is not present in properties, throws an exception.
        /// </summary>
        /// <typeparam name="T">Type of month page area</typeparam>
        /// <exception cref="NotSupportedException"></exception>
        public bool GetValueForArea<T>(T area = null) where T : MonthPageArea
        {
            var transferDataType = GetType();
            var pageAreaType = area?.GetType() ?? typeof(T);
            foreach (var property in transferDataType.GetProperties())
            {
                var areaAttribute = property.GetCustomAttribute<PageAreaAttribute>();
                if (areaAttribute?.AreaType == pageAreaType)
                    return (bool)property.GetValue(this);
            }
            throw new NotSupportedException($"Type {pageAreaType} of page area is not expected at transfer data model!");
        }
    }
}
