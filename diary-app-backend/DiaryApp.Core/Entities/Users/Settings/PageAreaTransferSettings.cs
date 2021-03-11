namespace DiaryApp.Core.Entities.Users.Settings
{
    /// <summary>
    /// Represents settings for automatically transferring page areas data when next month page is created.
    /// </summary>
    public class PageAreaTransferSettings : AppSettings
    {
        public bool TransferPurchasesArea { get; set; }
        public bool TransferDesiresArea { get; set; }
        public bool TransferIdeasArea { get; set; }
        public bool TransferGoalsArea { get; set; }
    }
}
