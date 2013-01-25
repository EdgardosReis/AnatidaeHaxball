using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class RepositoryLocator
    {
        private readonly static AHDBEntities _repo = new AHDBEntities();

        public static AHDBEntities Get()
        {
            return _repo;
        }
    }
}