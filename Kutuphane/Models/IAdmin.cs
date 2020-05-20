using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Models
{
    interface IAdmin
    {
        Boolean AddBook(string bookName, Byte[] image);
        DateTime SetTime(int day);
        List<StandardUser> GetUsers();

    }
}
