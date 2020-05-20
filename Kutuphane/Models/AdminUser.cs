using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kutuphane.Models
{
    public class AdminUser : User, IAdmin
    {
       
        public bool AddBook(string bookName, byte[] image)
        {
            throw new NotImplementedException();
        }

        public List<StandardUser> GetUsers()
        {
            Kutuphane.Context.Context db = new Kutuphane.Context.Context();
            List<StandardUser> UserList = new List<StandardUser>();
            UserList.AddRange(db.standardUsers.ToList());
            return UserList;

        }

        public StandardUser GetUserDetail(string id)
        {
            Kutuphane.Context.Context db = new Kutuphane.Context.Context();
            StandardUser user = db.standardUsers.Find(id);
           
            return user;
        }

        public DateTime SetTime(int day)
        {
            throw new NotImplementedException();
        }
    }
}