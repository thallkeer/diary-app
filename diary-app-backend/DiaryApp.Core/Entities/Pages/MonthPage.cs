using DiaryApp.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Entities
{
    public class MonthPage : PageBase
    {
        public MonthPage() : base()
        {
        }

        public MonthPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        [Required]
        public virtual PurchasesArea PurchasesArea { get; set; }
        [Required]
        public virtual DesiresArea DesiresArea { get; set; }
        [Required]
        public virtual IdeasArea IdeasArea { get; set; }
        [Required]
        public virtual GoalsArea GoalsArea { get; set; }        

        public override void CreateAreas()
        {
            DesiresArea = new DesiresArea(this, true);
            GoalsArea = new GoalsArea(this, true);
            PurchasesArea = new PurchasesArea(this, true);
            IdeasArea = new IdeasArea(this, true);
        }

        /// <summary>
        /// Creates new month page for next month
        /// </summary>
        /// <param name="transferDataModel">Model that represents which page areas need to be transferred</param>
        /// <returns></returns>
        public MonthPage TransferDataToNextMonth(TransferDataModel transferDataModel)
        {
            if (transferDataModel == null)
                throw new ArgumentNullException(nameof(transferDataModel));
            var nextPage = new MonthPage(Year, Month + 1, User);

            //initialize only non transferring areas
            nextPage.DesiresArea = new DesiresArea(nextPage, !transferDataModel.TransferDesiresArea);
            nextPage.GoalsArea = new GoalsArea(nextPage, !transferDataModel.TransferGoalsArea);
            nextPage.PurchasesArea = new PurchasesArea(nextPage, !transferDataModel.TransferPurchasesArea);
            nextPage.IdeasArea = new IdeasArea(nextPage, !transferDataModel.TransferIdeasArea);

            TransferAreasIfNecessary(transferDataModel, nextPage);

            return nextPage;
        }

        /// <summary>
        /// Transfer page areas data from passed month page
        /// </summary>
        /// <param name="transferDataModel">Transfer data model</param>
        /// <param name="monthPage">Month page</param>
        public void MergePageAreas(TransferDataModel transferDataModel, MonthPage monthPage)
        {
            if (transferDataModel == null)
                throw new ArgumentNullException(nameof(transferDataModel));
            if (monthPage == null)
                throw new ArgumentNullException(nameof(monthPage));
            monthPage.TransferAreasIfNecessary(transferDataModel, this);
        }

        private void TransferAreasIfNecessary(TransferDataModel transferDataModel, MonthPage nextPage)
        {
            nextPage.GoalsArea.TransferAreaDataIfNeeded(transferDataModel, GoalsArea);
            nextPage.DesiresArea.TransferAreaDataIfNeeded(transferDataModel, DesiresArea);
            nextPage.PurchasesArea.TransferAreaDataIfNeeded(transferDataModel, PurchasesArea);
            nextPage.IdeasArea.TransferAreaDataIfNeeded(transferDataModel, IdeasArea);
        }
    }
}
