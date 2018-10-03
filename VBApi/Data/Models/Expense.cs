using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBApi.Data.Models
{
	public class Expense
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime TransactionDate { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
		public double Amount { get; set; }

        [MaxLength(150)]
		public string Recipient { get; set; }

        [MinLength(3, ErrorMessage = "Use ISO 3-Code currency code")]
		[MaxLength(3, ErrorMessage = "Use ISO 3-Code currency code")]
		public string Currency { get; set; }

        [MaxLength(50)]
        public string ExpenseType { get; set; }

    }
}
