using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Models
{
    interface IStandardUser
    {
        List<Book> SearchBooksByIsbn(int Isbn);
        List<Book> SearchBooksByName(string Name);

        Boolean BorrowBook(int Isbn);

        Boolean DeliverBook(Byte[] image);
    }
}
