using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kutuphane.Models
{
    public class StandardUser : User, IStandardUser
    {
        
        public virtual ICollection<UserBookMap> userBooks { get; set; }

        public bool BorrowBook(int Isbn)
        {
            throw new NotImplementedException();
        }

        public bool DeliverBook(byte[] image)
        {
            throw new NotImplementedException();
        }

        public List<Book> SearchBooksByIsbn(int Isbn)
        {
            throw new NotImplementedException();
        }

        public List<Book> SearchBooksByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}