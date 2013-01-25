using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class JogadorRepositoryLocator
    {
        private readonly static JogadorRepository _repo = new JogadorRepository();

        public static JogadorRepository Get()
        {
            return _repo;
        }
    }
}