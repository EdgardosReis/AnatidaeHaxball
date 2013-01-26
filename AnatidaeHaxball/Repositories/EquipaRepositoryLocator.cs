using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class EquipaRepositoryLocator
    {
        private readonly static EquipaRepository _repo = new EquipaRepository();

        public static EquipaRepository Get()
        {
            return _repo;
        }
    }
}