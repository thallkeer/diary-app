using System.ComponentModel.DataAnnotations;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.API.Requests
{
    public class CreatePurchaseListRequest
    {
        [Required]
        public TodoListDto TodoList { get; set; }
        [Required]
        public int PurchasesAreaId { get; set; }
    }
}
