using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class RoleManager
    {
        public enum eRole
        {
            Unknown = 0
            //Administrator = 1,
            //Customer = 2,
            //Executive = 3,
            //Serviceman = 4
        }

        public User User { get; }
        public eRole Role { get; }

        public RoleManager(User user)
        {
            User = user;

            if (user != null)
            {
                Role = (eRole)user.RoleId;
            }
            else
            {
                Role = eRole.Unknown;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return Role == eRole.Administrator;
            }
        }

        //public bool IsCustomer
        //{
        //    get
        //    {
        //        return Role == eRole.Customer;
        //    }
        //}

        //public bool IsExecutive
        //{
        //    get
        //    {
        //        return Role == eRole.Executive;
        //    }
        //}

        //public bool IsServiceman
        //{
        //    get
        //    {
        //        return Role == eRole.Serviceman;
        //    }
        //}

        //public bool IsUnknown
        //{
        //    get
        //    {
        //        return Role == eRole.Unknown;
        //    }
        //}
    }
}
