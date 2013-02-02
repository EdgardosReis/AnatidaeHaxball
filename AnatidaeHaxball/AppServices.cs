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
        private static readonly EquipaRepository _equipaRepo = EquipaRepositoryLocator.Get();
        private static readonly LogJogadorRepository _logJogRepo = LogJogadorRepositoryLocator.Get();
        private static readonly CompeticaoRepository _competicaoRepo = CompeticaoRepositoryLocator.Get();

        public static IEnumerable<Jogador> GetAllJogadores()
        {
            return _jogadorRepo.GetAll();
        }

        public static IEnumerable<Jogador> GetAllJogadoresDaEquipa()
        {
            return _jogadorRepo.GetAll().Where(j => j.naEquipa);
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

            jogador.naEquipa = true;
            _jogadorRepo.Add(jogador);
            
            Jogador jog = _jogadorRepo.GetAll().FirstOrDefault(
                j => j.nome == jogador.nome &&
                     j.avatar == jogador.avatar &&
                     j.posicao == jogador.posicao);

            if (jog != null)
            {
                LogJogadores lj = new LogJogadores { dataEntrada = DateTime.Now, jogadorID = jog.idJogador };

                _logJogRepo.Add(lj);
            }

        }

        internal static void RemoveJogador(int id)
        {
            _jogadorRepo.Remove(id);
        }

        internal static void RemoveJogadorFromTeam(int id, string notas)
        {
            LogJogadores existingLog = _logJogRepo.GetAll().FirstOrDefault(l => l.jogadorID == id && l.dataSaida == null && l.dataEntrada != null);
            if (existingLog != null)
            {
                existingLog.dataSaida = DateTime.Now;
                existingLog.notas = notas;
                _logJogRepo.Add(existingLog);
            }
            else
            {
                LogJogadores lj = new LogJogadores { dataSaida = DateTime.Now, jogadorID = id, notas = notas };
                _logJogRepo.Add(lj);
            }

            Jogador j = GetJogador(id);
            j.naEquipa = false;

            _jogadorRepo.Edit(j);
        }

        //***********************************// LOG JOGADORES //***********************************//
        



        //**************************************// EQUIPAS //**************************************//

        public static IEnumerable<Equipa> GetAllEquipas()
        {
            return _equipaRepo.GetAll();
        }

        public static Equipa GetEquipa(int id)
        {
            return _equipaRepo.GetById(id);
        }

        public static void EditEquipa(Equipa equipa)
        {
            _equipaRepo.Edit(equipa);
        }

        public static void AddEquipa(Equipa equipa)
        {
            equipa.activa = true;
            _equipaRepo.Add(equipa);
        }

        internal static void RemoveEquipa(int id)
        {
            _equipaRepo.Remove(id);
        }

        internal static IEnumerable<Equipa> GetAllEquipasActivas()
        {
            return _equipaRepo.GetAll().Where(e => e.activa);
        }

        internal static void DeactivateEquipa(int id)
        {
            Equipa e = GetEquipa(id);
            e.activa = false;
            EditEquipa(e);
        }

        //************************************// COMPETICOES //************************************//

        internal static IEnumerable<Competicao> GetAllCompeticoes()
        {
            return _competicaoRepo.GetAll();
        }

        internal static IEnumerable<Competicao> GetActiveCompeticoes()
        {
            return GetAllCompeticoes().Where(c => c.activa);
        }

        internal static IEnumerable<Competicao> GetInactiveCompeticoes()
        {
            return GetAllCompeticoes().Where(c => !c.activa);
        }

        internal static Competicao GetCompeticao(int id)
        {
            return _competicaoRepo.GetById(id);
        }

        internal static void AddCompeticao(Competicao competicao)
        {
            competicao.activa = true;
            _competicaoRepo.Add(competicao);
        }

        internal static void EditCompeticao(Competicao competicao)
        {
            _competicaoRepo.Edit(competicao);
        }

        internal static void DeactivateCompeticao(int id)
        {
            Competicao competicao = GetCompeticao(id);
            competicao.activa = false;
            EditCompeticao(competicao);
        }
    }
}