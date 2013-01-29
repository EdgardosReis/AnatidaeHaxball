using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Repositories
{
    public class JogadorRepository : IRepository<Jogador>
    {

        private readonly AHDBEntities _repo = RepositoryLocator.Get();

        public IEnumerable<Jogador> GetAll()
        {
            return _repo.Jogador;
        }

        public IEnumerable<Jogador> GetSome(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public Jogador GetById(int id)
        {
            return _repo.Jogador.First(j => j.idJogador == id);
        }

        public void Add(Jogador t)
        {
            _repo.Jogador.Add(t);

            _repo.SaveChanges();
        }

        public void Edit(Jogador t)
        {
            Jogador jogador = _repo.Jogador.First(j => j.idJogador == t.idJogador);
            jogador.avatar = t.avatar;
            jogador.nome = t.nome;
            jogador.posicao = t.posicao;
            jogador.naEquipa = t.naEquipa;
            jogador.nomeShirt = t.nomeShirt;
            jogador.ave = t.ave;

            _repo.SaveChanges();
        }

        public void Remove(Jogador t)
        {
            _repo.Jogador.Remove(t);

            _repo.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _repo.Jogador.Contains(GetById(id));
        }
    }
}