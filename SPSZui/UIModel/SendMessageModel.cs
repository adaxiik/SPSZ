using System;
using System.ComponentModel.DataAnnotations;

namespace SPSZui.UIModel
{
    public class SendMessageModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }

        [Required (ErrorMessage = "Příjemce je povinný údaj")]
        public int RecipientId { get; set; }

    }
    
}
