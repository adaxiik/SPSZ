using System;
using System.ComponentModel.DataAnnotations;

namespace SPSZui.UIModel
{
    public class SendMessageModel
    {
        [Required (ErrorMessage = "Předmět je povinný údaj")]
        public string Subject { get; set; }
        [Required (ErrorMessage = "Zpráva je povinný údaj")]
        public string Message { get; set; }

        [Required (ErrorMessage = "Příjemce je povinný údaj")]
        public int RecipientId { get; set; }
        public int SenderId { get; set; }

    }
    
}
