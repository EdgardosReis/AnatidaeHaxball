using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class LogJogadorRepository : IRepository<LogJogadores>
    {
        private readonly AHDBEntities _repo = RepositoryLocator.Get();

        public IEnumerable<LogJogadores> GetAll()
        {
            return _repo.LogJogadores;
        }

        public IEnumerable<LogJogadores> GetSome(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public LogJogadores GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(LogJogadores t)
        {
            _repo.LogJogadores.Add(t);

            _repo.SaveChanges();
        }

        public void Edit(LogJogadores t)
        {
            LogJogadores lj = _repo.LogJogadores.First(j => j.jogadorID == t.jogadorID && j.dataEntrada == t.dataEntrada);
            lj.notas = t.notas;

            _repo.SaveChanges();
        }

        public void Remove(LogJogadores t)
        {
            _repo.LogJogadores.Remove(t);
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }
    }
}