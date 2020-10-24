using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class User : IAdmin
    {
        // Användar metoder för både admin och mekaniker
        // Saknar metoder för tillfället men ska fyllas i beroende på om vi använder dictionary eller annat
        public void AddUser()
        {
            throw new NotImplementedException();
        }

        public void GetArenden()
        {
            throw new NotImplementedException();
        }

        public void GetUser()
        {
            throw new NotImplementedException();
        }

        public void RemoveArenden()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser()
        {
            throw new NotImplementedException();
        }
    }
}
