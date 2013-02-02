using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class CompeticaoRepository : IRepository<Competicao>
    {
        private readonly AHDBEntities _repo = RepositoryLocator.Get();

        public IEnumerable<Competicao> GetAll()
        {
            return _repo.Competicao;
        }

        public IEnumerable<Competicao> GetSome(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Competicao GetById(int id)
        {
            return _repo.Competicao.Find(id);
        }

        public void Add(Competicao t)
        {
            _repo.Competicao.Add(t);

            _repo.SaveChanges();
        }

        public void Edit(Competicao t)
        {
            Competicao competicao = _repo.Competicao.Find(t.competicaoID);
            competicao.edicao = t.edicao;
            competicao.imagem = t.imagem;
            competicao.link = t.link;
            competicao.nome = t.nome;

            _repo.SaveChanges();
        }

        public void Remove(object t)
        {
            _repo.Competicao.Remove(_repo.Competicao.Find(t));

            _repo.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _repo.Competicao.Contains(GetById(id));
        }
    }
}