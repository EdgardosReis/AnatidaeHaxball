using AnatidaeHaxball.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball
{
    public class AppServices
    {
        private static readonly JogadorRepository _jogadorRepo = JogadorRepositoryLocator.Get();

        public static IEnumerable<Jogador> GetAllJogadores()
        {
            return _jogadorRepo.GetAll();
        }

        public static Jogador GetJogador(int id)
        {
            return _jogadorRepo.GetById(id);
        }

        public static void EditJogador(Jogador jogador)
        {
            _jogadorRepo.Edit(jogador);
        }

        public static void AddJogador(Jogador jogador)
        {
            _jogadorRepo.Add(jogador);
        }

        internal static void RemoveJogador(int id)
        {
            _jogadorRepo.Remove(GetJogador(id));
        }
    }
}