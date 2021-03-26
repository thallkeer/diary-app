using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Users.Settings;

namespace DiaryApp.Core.Extensions
{
    public static class PageAreaExtensions
    {
        /// <summary>
        /// Transfer data from passed area to this area if it's needed according to the transfer model
        /// </summary>
        /// <typeparam name="T">Type of month page area</typeparam>
        /// <param name="pageAreaReceiver">Page area that will receive data</param>
        /// <param name="transferDataModel">Transfer model</param>
        /// <param name="pageAreaSource">Page area source of data</param>
        public static void TransferAreaDataIfNeeded<T>(this T pageAreaReceiver, PageAreaTransferSettings transferDataModel, T pageAreaSource) where T : MonthPageArea, IMonthPageArea<T>
        {
            bool transfer = transferDataModel.GetValueForArea<T>();
            if (transfer)
                pageAreaReceiver.AddDataFromOtherArea(pageAreaSource);
        }
    }
}
