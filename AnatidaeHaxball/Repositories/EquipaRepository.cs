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

        public IEnumerable<Equipa> GetSome(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Equipa GetById(int id)
        {
            return _repo.Equipa.Find(id);
        }

        public void Add(Equipa t)
        {
            _repo.Equipa.Add(t);

            _repo.SaveChanges();
        }

        public void Edit(Equipa t)
        {
            Equipa equipa = _repo.Equipa.Find(t.idEquipa);
            equipa.activa = t.activa;
            equipa.logo = t.logo;
            equipa.nome = t.nome;
            equipa.tag = t.tag;

            _repo.SaveChanges();
        }

        public void Remove(object t)
        {
            _repo.Equipa.Remove(_repo.Equipa.Find(t));

            _repo.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _repo.Equipa.Contains(GetById(id));
        }
    }
}