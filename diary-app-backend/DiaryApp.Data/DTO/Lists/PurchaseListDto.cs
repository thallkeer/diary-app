using DiaryApp.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Data.DTO
{
    public class PurchaseListDto : ListWrapperDto
    {
        public TodoListDto List { get; set; }        
    }
}
