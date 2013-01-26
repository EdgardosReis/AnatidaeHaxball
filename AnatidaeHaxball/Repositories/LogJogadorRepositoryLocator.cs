using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class LogJogadorRepositoryLocator
    {
        private readonly static LogJogadorRepository _repo = new LogJogadorRepository();

        public static LogJogadorRepository Get()
        {
            return _repo;
        }
    }
}