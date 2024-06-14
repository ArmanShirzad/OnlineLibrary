using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Blazor.Models
{
    public class LoanDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public UserDto User { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
    public class BorrowBookDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
