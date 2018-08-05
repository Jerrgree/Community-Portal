using System;
using System.Collections;

namespace Domain
{
    class Program
    {
        private CommunityContext context;
        static void Main(string[] args)
        {
            var context = new CommunityContext();

            CreateUserRoles();
        }

        static void CreateUserRoles()
        {

        }
    }
}
