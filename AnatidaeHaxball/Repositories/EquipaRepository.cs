using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class EquipaRepository : IRepository<Equipa>
    {

        private readonly AHDBEntities _repo = RepositoryLocator.Get();

        public IEnumerable<Equipa> GetAll()
        {
            return _repo.Equipa;
        }

        public IEnumerable<Equipa> GetSome(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public Equipa GetById(int id)
        {
            return _repo.Equipa.First(e => e.idEquipa == id);
        }

        public void Add(Equipa t)
        {
            _repo.Equipa.Add(t);

            _repo.SaveChanges();
        }

        public void Edit(Equipa t)
        {
            Equipa equipa = _repo.Equipa.First(e => e.idEquipa == t.idEquipa);
            equipa.activa = t.activa;
            equipa.logo = t.logo;
            equipa.nome = t.nome;
            equipa.tag = t.tag;

            _repo.SaveChanges();
        }

        public void Remove(Equipa t)
        {
            _repo.Equipa.Remove(t);

            _repo.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _repo.Equipa.Contains(GetById(id));
        }
    }
}