using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class CompeticaoRepositoryLocator
    {
        private readonly static CompeticaoRepository _repo = new CompeticaoRepository();

        public static CompeticaoRepository Get()
        {
            return _repo;
        }
    }
}