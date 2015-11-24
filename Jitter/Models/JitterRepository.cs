using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jitter.Models
{
    public class JitterRepository
    {
        private JitterContext _context;
        public JitterContext Context { get {return _context;}}

        public JitterRepository()
        {
            _context = new JitterContext();
        }

        public JitterRepository(JitterContext a_context)
        {
            _context = a_context;
        }

        public List<JitterUser> GetAllUsers()
        {
            var query = from users in _context.JitterUsers select users;
            return query.ToList();
        }

        public JitterUser GetUserByHandle(string handle)
        {
            var query = from users in _context.JitterUsers where users.Handle == handle select users;

            query.Single();

            throw new NotImplementedException();
        }

        public bool IsHandleAvailable(string handle)
        {
            bool available = false;
            try
            {
                JitterUser some_user = GetUserByHandle(handle);
                if (some_user == null)
                {
                    available = true;
                }
            }
            catch(InvalidOperationException)
            {
                
            }
            
            return available;
        }

        public List<JitterUser> SearchByHandle(string handle)
        {
            var query = from users in _context.JitterUsers where users.Handle == handle select users;
            List<JitterUser> found_users = query.Where(user => user.Handle.Contains(handle)).ToList();
            return found_users.Sort();
        }
    }
}